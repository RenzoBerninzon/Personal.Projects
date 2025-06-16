using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Store.BusinessMS.Users.Application.Wrappers
{
    public class Response<T>
    {
        public bool Succeeded { get; set; }

        public string? Message { get; set; }

        public T? Data { get; set; }

        public List<Error>? Errors { get; set; }

        protected Response(bool succeded, T data, string? message = null)
        {
            Succeeded = succeded;
            Message = message;
            Data = data;
        }

        protected Response(bool succeded, string? message = null)
        {
            Succeeded = succeded;
            Message = message;
        }

        public static Response<T> Success(T value)
        {
            return new Response<T>(succeded: true, value);
        }

        public static Response<T> Failure(string? message)
        {
            return new Response<T>(succeded: false, message);
        }
    }
}
