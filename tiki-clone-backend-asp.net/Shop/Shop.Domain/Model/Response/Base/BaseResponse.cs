using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Model.Response.Base
{
    public class BaseResponse<T>
    {
        public string Message { get; set; } = "Success";

        public string TraceId { get; set; } = "";

        public T? Data { get; set; }
    }
}
