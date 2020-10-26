using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Response
    {
		public object data
		{
			get;
			set;
		}

		public bool success
		{
			get;
			set;
		}

		public string msg
		{
			get;
			set;
		}

		public int status
		{
			get;
			set;
		}
		public Response()
		{

		}

		public Response(bool success)
		{
			this.success = success;
		}

		public Response(bool success, string message)
		{
			this.success = success;
			this.msg = message;
		}

		public Response(bool success, string message, object data)
		{
			this.success = success;
			this.data = data;
			this.msg = message;
		}

		public Response(Exception ex)
		{
			this.success = false;
			this.msg = ex.Message;
		}
	}
}
