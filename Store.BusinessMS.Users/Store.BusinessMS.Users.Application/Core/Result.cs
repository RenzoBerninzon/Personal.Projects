using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BusinessMS.Users.Application.Core
{
    public class Result
    {
        public bool Succeded { get; set; }
        public string Message { get; set; } = string.Empty;

        public static Result Success(string message = "Success") => new Result
        {
            Succeded = true,
            Message = message
        };

        public static Result Failure(string message = "Failure") => new Result
        {
            Succeded = false,
            Message = message
        };
    }
}
