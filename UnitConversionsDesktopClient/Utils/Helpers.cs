using System;

namespace UnitConversionsDesktopClient.Utils
{
    public static class Helpers
    {
        public static string GetEndpointRootUrl(bool isProduction)
        {
            string endpointRootUrl = string.Empty;
            if (isProduction)
            {
                endpointRootUrl = Constants.ConversionServiceEndPoints.PROD_UNIT_CONVERSION_ENDPOINT;
            }
            else
            {
                endpointRootUrl = Constants.ConversionServiceEndPoints.DEV_UNIT_CONVERSION_ENDPOINT;
            }
            return endpointRootUrl;
        }
    }
}
