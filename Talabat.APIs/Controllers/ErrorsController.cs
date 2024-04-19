using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;

namespace Talabat.APIs.Controllers
{
	[Route("errors/{code}")]
	[ApiController]
	[ApiExplorerSettings(IgnoreApi = true)]
	public class ErrorsController : ControllerBase
	{

		public ActionResult Error(int code)
		{
			if (code == 404)
				return NotFound(new ApiResponse(code));
			else if (code == 401)
				return Unauthorized(new ApiResponse(401));
			else if (code == 400)
				return BadRequest(400);
			else
				return StatusCode(code);

		}
	}
}
