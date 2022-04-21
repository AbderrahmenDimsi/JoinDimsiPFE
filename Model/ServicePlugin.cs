using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Model
{
  public  class Serviceplugin
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
            RegexOptions.CultureInvariant | RegexOptions.Singleline);
            bool isValidEmail = regex.IsMatch(email);
            return isValidEmail;
        }


        public static bool IsValidTelephone(string telephone)
        {
            if (string.IsNullOrWhiteSpace(telephone))
                return false;
            Regex regex = new Regex(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{2}[-\s\.]?[0-9]{4,6}$",
            RegexOptions.CultureInvariant | RegexOptions.Singleline) ;
            bool IsValidTelephone = regex.IsMatch(telephone.ToString());
            return IsValidTelephone;
        }


    }
}
