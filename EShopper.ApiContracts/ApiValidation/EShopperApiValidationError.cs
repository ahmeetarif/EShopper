using System;
using System.Collections.Generic;

namespace EShopper.ApiContracts.ApiValidation
{
    public class EShopperApiValidationError : Exception
    {
        public List<EShopperApiError> EShopperValidationErrors { get; set; } = new List<EShopperApiError>();
    }

    public class EShopperApiError
    {
        public string FieldName { get; set; }
        public string Message { get; set; }
    }
}