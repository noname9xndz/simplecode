using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLGraphTypeFirstNestedTable.Models.Responses
{
    public class ResponseModel<T>
    {
        public T Data { get; set; }

        public List<ErrorModel> Errors { get; set; }

        public void ThrowExceptionOnError()
        {
            if (Errors != null && Errors.Any())
            {
                throw new ApplicationException($"Message: {Errors[0].Message} Code: {Errors[0].Code}");
            }
        }
    }
}
