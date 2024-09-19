using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infarstuructre.ViewModel
{
	public class ApiResponse
	{
		public ApiResponse()
		{
			ErrorMessage = new List<string>();
		}

		public HttpStatusCode StatusCode  { get; set; } = HttpStatusCode.OK;
		public bool IsSuccess { get; set; } = true;
		public List<string> ErrorMessage { get; set; }
		public object Result { get; set; }
	}
}
