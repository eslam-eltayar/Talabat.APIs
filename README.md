Talabat Web API

Introduction Talabat is Web API used for shopping online where you can create an account, login, preview and filter products, adding products to basket, creating orders.

Project Support Features

Users can signup and login to their accounts Public (non-authenticated) users can access the index page where they can view the products Authenticated users can create orders and pay for the order

API Endpoints

| HTTP Verbs | Endpoints | Action |

| POST | /api/Accounts/Register | To sign up a new user account | | POST | /api/Accounts/Login | To login an existing user account | | GET | /api/Accounts/GetCurrentUser | To get the current loged user | | GET | /api/Accounts/Address | To get the address of the loged user | | PUT | /api/Accounts/Address | To edit the address of the loged user | | GET | /api/Accounts/emailExists/{email} | To chech if the email exists |

| GET | /api/Basket/{basketId} | To get a basket | | POST | /api/Basket | To add a basket | | DELETE | /api/Basket/{basketId} | To delete a basket |

| POST | /api/Orders | To add an order | | GET | /api/Orders | To get all orders | | GET | /api/Orders/{id} | To get a specific order | | GET | /api/Orders/DeliveryMethods | To get the delivery methods |

| POST | /api/Payments/{basketId} | To create payment for a selected basket |

| GET | /api/Products | To get all products | | GET | /api/Products/{id} | To get a product by its id | | GET | /api/Products/Types | To get all products types | | GET | /api/Products/Brands | To get all products brands |

Technologies Used

SQL Server This is a is a relational database management system (RDBMS).
ASP.NET Core This is a cross-platform, high-performance, open-source framework for building modern, cloud-enabled, Internet-connected apps.
Entity Framework Core This is a modern object-database mapper for . NET.
Auto Mapper This is a library used to map data from one object to another.
