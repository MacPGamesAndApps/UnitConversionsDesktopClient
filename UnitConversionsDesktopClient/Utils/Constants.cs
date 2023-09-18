namespace UnitConversionsDesktopClient.Utils
{
    public static class Constants
    {
        public static class Messages
        {
            public static readonly string ERROR_MICROSERVICE_NOT_RESPONDING = "The Microservice is not responding. Please, make sure it is running and working properly.";
        }

        public static class ConversionServiceEndPoints
        {
            public static readonly string DEV_UNIT_CONVERSION_ENDPOINT = "http://localhost:60937/";
            public static readonly string PROD_UNIT_CONVERSION_ENDPOINT = "https://macpunitconversionsmicroservice.azurewebsites.net/";
        }
    }
}
