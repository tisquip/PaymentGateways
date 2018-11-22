using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace PaymentsGateways.PayNow
{
    public static class Extentions
    {
        public static string UrlEncode(this string input)
        {
            if (String.IsNullOrWhiteSpace(input))
            {
                return input;
            }

            return HttpUtility.UrlEncode(input);
        }

        public static string UrlDecode(this string input)
        {
            if (String.IsNullOrWhiteSpace(input))
            {
                return input;
            }

            return HttpUtility.UrlDecode(input);
        }

        public static bool IsNullOrWhiteSpace(this string stringToCheck)
        {
            return String.IsNullOrWhiteSpace(stringToCheck);
        }

        public static string IsNullOrWhiteSpaceReplace(this string stringToCheck, string stringToReplace = "")
        {
            if (stringToCheck.IsNullOrWhiteSpace())
            {
                return stringToReplace;
            }
            return stringToCheck;
        }

        public static string DateDisplay(this DateTime dateTime)
        {
            return $"{dateTime.ToLongDateString()} {dateTime.ToShortTimeString()}";
        }

        public static string DateOrEmptyString(this DateTime dateTime)
        {
            DateTime dateTimeToCompare = new DateTime(2018, 1, 1);
            if (dateTime > dateTimeToCompare)
            {
                return dateTime.DateDisplay();
            }
            return "";

        }
    }
}
