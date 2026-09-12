using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Shared.Common
{
    public class ResponseModel<T> where T : class
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
    }
}
