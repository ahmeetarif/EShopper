using EShopper.Core.Business.Models;
using System.Collections.Generic;

namespace EShopper.Contracts.V1.Responses.Error
{
    public class ErrorResponseModel
    {
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
    }
}