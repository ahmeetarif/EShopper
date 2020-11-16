using System;

namespace EShopper.ApiContracts.ApiExceptions
{
    public class EShopperApiException : Exception
    {
        public EShopperApiException()
        {

        }
        public EShopperApiException(string message)
            : base(message)
        {

        }
        public EShopperApiException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}