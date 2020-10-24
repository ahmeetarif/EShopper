using System;

namespace EShopper.Common.Exceptions
{
    public class EShopperException : Exception
    {
        public EShopperException()
        {

        }
        public EShopperException(string message)
            : base(message)
        {

        }
        public EShopperException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}