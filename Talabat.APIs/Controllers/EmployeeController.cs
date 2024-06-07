using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications.Employee_Specs;

namespace Talabat.APIs.Controllers
{
	
	public class EmployeeController : BaseApiController
	{
		private readonly IGenericRepository<Employee> _employeeRepo;

		public EmployeeController(IGenericRepository<Employee> employeeRepo)
        {
			_employeeRepo = employeeRepo;
		}


		[HttpGet] // GET : /api/Employees 
		public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
		{
			var spec = new EmployeeWithDepartmentSpecification();

			var employee = await _employeeRepo.GetAllWithSpecAsync(spec);

			return Ok(employee);
		}

		[HttpGet("{id}")] // GET : /api/Employees/1
		public async Task<ActionResult<Employee>> GetEmployee(int id)
		{
			var spec = new EmployeeWithDepartmentSpecification(id);

			var employee = await _employeeRepo.GetByIdWithSpecAsync(spec);

			if (employee is null)
				return NotFound(new ApiResponse(404));

			return Ok(employee);
		}

	}
}
