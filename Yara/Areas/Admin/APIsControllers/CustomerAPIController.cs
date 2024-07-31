using Domin.Entity;
using System.Linq.Expressions;
using System.Net;

namespace Yara.Areas.Admin.API_Controller;

[Authorize(Roles = "Admin,ApiRoles")]
[Route("api/[controller]")]
[ApiController]
public class CustomerAPIController : ControllerBase
{
	//private ApiResponse _response;
	//public CustomerAPIController(IICustomer iCustomer)
	//{
	//	this.iCustomer = iCustomer;
	//	_response = new ApiResponse();
	//}

	//private readonly IICustomer iCustomer;

 //   [HttpPost("GitAllCustomers/{start}/{end}")]
 //   public async Task<ActionResult<IEnumerable<TBViewCustomers>>> GitAllCustomers(int start, int end)
 //   {
 //       try
 //       {

	//		var customers = await iCustomer.GetAllCustomersAsync(start, end);
	//		if (customers == null)
	//			_response.StatusCode = HttpStatusCode.BadRequest;

 //           _response.Result = customers;
 //           _response.StatusCode = HttpStatusCode.Created;


	//		return Ok(_response);

	//	}

 //       catch (Exception ex)
 //       {
 //           _response.IsSuccess = false;
 //           _response.ErrorMessage = new List<string> { ex.Message };
 //       }
 //       return Ok(_response);
 //   }

	

	//[HttpPost("GitAllCustomersWithCondition")]
 //   public async Task<ActionResult<IEnumerable<TBViewCustomers>>> GitAllCustomersWithCondition(Expression<Func<TBViewCustomers, bool>> condition)
 //   {
 //       try
 //       {
 //           var customers = await iCustomer.GetAllCustomersWithConditionAsync(condition);
 //           if (customers == null)
 //               _response.StatusCode = HttpStatusCode.BadRequest;
 //           _response.Result = customers;
 //           _response.StatusCode = HttpStatusCode.Created;

 //           return Ok(_response);
 //       }
 //       catch (Exception ex)
 //       {
 //           _response.IsSuccess = false;
 //           _response.ErrorMessage = new List<string> { ex.Message };
 //       } 
 //       return Ok(_response);
 //   }

 //   [HttpPost("{id}")]
 //   public async Task<ActionResult<Customer>> GitCustomerById(int id)
 //   {
 //       try
 //       {
 //           var customer = await iCustomer.GetCustomerAsyncview(id);
 //           if (customer == null)
 //               _response.StatusCode = HttpStatusCode.BadRequest;
 //           _response.Result = customer;
 //           _response.StatusCode = HttpStatusCode.Created;

 //           return Ok(_response);
 //       }
 //       catch (Exception ex)
 //       {
 //           _response.IsSuccess = false;
 //           _response.ErrorMessage = new List<string> { ex.Message };
 //       }

 //       return Ok(_response);
	//}
	//[HttpGet("{name}")]
	//public async Task<ActionResult<TBViewCustomers>> GitCustomerByNaame(string name)
	//{
	//	try
	//	{
	//		var customer = await iCustomer.GetCustomerAsyncviewName(name);
	//		if (customer == null)
	//		{
	//			_response.StatusCode = HttpStatusCode.BadRequest;
	//			return Ok(_response);
	//		}

	//		_response.Result = customer;
	//		_response.StatusCode = HttpStatusCode.Created;
	//		return Ok(_response);
	//	}
	//	catch (Exception ex)
	//	{
	//		_response.IsSuccess = false;
	//		_response.ErrorMessage = new List<string> { ex.Message };
	//		return StatusCode(500, _response);
	//	}
	//}
	//[HttpPost]
 //   public async Task<ActionResult> AddCustomer(Customer customer)
 //   {
 //       try
 //       {
 //           if (!ModelState.IsValid)
 //               _response.StatusCode = HttpStatusCode.BadRequest;

 //           await iCustomer.AddCustomerAsync(customer);

 //           _response.Result = customer;
 //           return Ok(_response);
 //       }
 //       catch (Exception ex)
 //       {
 //           _response.IsSuccess = false;
 //           _response.ErrorMessage = new List<string> { ex.Message };
 //       }

 //       return Ok(_response);
 //   }

 //   [HttpPut("{id}")]
 //   public async Task<ActionResult> EditCustomer(int id, [FromBody] Customer customer)
 //   {
 //       try
 //       {
	//		var custom = await iCustomer.GetCustomerAsync(id);
	//		if (custom == null)
	//			_response.StatusCode = HttpStatusCode.BadRequest;

	//		custom = customer;
	//		await iCustomer.UpdateCustomerAsync(custom);

	//		_response.Result = custom;
	//		return Ok(_response);
	//	}
 //       catch (Exception ex)
 //       {
 //           _response.IsSuccess = false;
 //           _response.ErrorMessage = new List<string> { ex.Message };
 //       }

 //       return Ok(_response);
 //   }

 //   [HttpDelete("{id}")]
 //   public async Task<ActionResult> DeleteCustomer(int id)
 //   {
 //       try
 //       {
	//		var custom = await iCustomer.GetCustomerAsync(id);
	//		if (custom == null)
	//			_response.StatusCode = HttpStatusCode.BadRequest;

	//		custom.CurrentState = false;
	//		await iCustomer.UpdateCustomerAsync(custom);

	//		_response.Result = custom;
	//		return Ok(_response);
	//	}
 //       catch (Exception ex)
	//	{
	//		_response.IsSuccess = false;
	//		_response.ErrorMessage = new List<string> { ex.Message };
	//	}

	//	return Ok(_response);
 //   }
}

