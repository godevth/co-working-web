namespace CoreCMS.Application.Shared.Helpers
{
    public static class ConvertHelper
    {
        public static string ToDefaultString(this object input, string defaultVal = "")
        {
            if (input != null)
            {
                return input.ToString();
            }

            return defaultVal;
        }

        public static int ToDefaultInt(this object input, int defaultVal = 0)
        {
            try
            {
                return int.Parse(input.ToDefaultString(defaultVal.ToString()));
            }
            catch
            {
                return defaultVal;
            }
        }

        public static int ToDefaultInt(this string input, int defaultVal = 0)
        {
            try
            {
                return int.Parse(input);
            }
            catch
            {
                return defaultVal;
            }
        }

        public static decimal ToDefaultDecimal(this object input, decimal defaultVal = 0)
        {
            try
            {
                return decimal.Parse(input.ToDefaultString(defaultVal.ToString()));
            }
            catch
            {
                return defaultVal;
            }
        }

        public static decimal ToDefaultDecimal(this string input, decimal defaultVal = 0)
        {
            try
            {
                return decimal.Parse(input);
            }
            catch
            {
                return defaultVal;
            }
        }
    }
}