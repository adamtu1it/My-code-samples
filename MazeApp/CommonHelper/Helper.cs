using System;
using System.Configuration;
using CommonHelper.Properties;

namespace CommonHelper
{
    public class Helper
    {
        public static string GetValueFromConfigByKey(string key)
        {
            var value = ConfigurationManager.AppSettings[key];

            if (string.IsNullOrEmpty(value))
                throw new ApplicationException(string.Format("{0} {1}", key, Resources.NotFoundInConfig));

            return value;
        }
    }
}