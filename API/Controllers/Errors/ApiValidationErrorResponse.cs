using System.Collections.Generic;

namespace API.Controllers.Errors
{
    public class ApiValidationErrorResponse : ApiResponse
    {
        public ApiValidationErrorResponse() : base(400)
        {
        }
        public IEnumerable<string> Erros {get; set;}
    }
}