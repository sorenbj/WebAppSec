using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppSec
{
    public class InputValidate
    {
        public static bool ValidateUsername(string input)
        {
            bool pass = true;

            //var positiveIntRegEx = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z0-9]*$");

            //if (positiveIntRegEx.IsMatch(input) == false)
            //{
            //    pass = false;
            //}

            if (input.Length <= 4 || input.Length > 20)
            {
                pass = false;
            }

            return pass;
        }

        public static bool ValidateEmail(string email)
        {
            bool pass = true;

            int index1 = email.IndexOf("@");
            int index2 = email.LastIndexOf("@");

            int num = email.Split('@').Length - 1;

            if (email.Split('@').Length - 1 > 1)
            {
                pass = false;
            }

            if (index1 != index2)
            {
                pass = false;
            }

            if (email.Trim() == "")
            {
                pass = false;
            }

            return pass;
        }

        public static bool ValidatePassword(string input)
        {
            bool pass = true;

            var positiveIntRegEx = new System.Text.RegularExpressions.Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[$@$!%*?&amp;]){11,20}*$");

            if (positiveIntRegEx.IsMatch(input) == false)
            {
                pass = false;
            }

            return pass;
        }

        public static bool ComparePassword(string password, string comparePassword)
        {
            bool pass = true;

            if (password.Equals(comparePassword) == false)
            {
                pass = false;
            }

            if (comparePassword.Trim().Length < 1)
            {
                pass = false;
            }

            return pass;
        }

        public static bool ValidateName(string input)
        {
            bool pass = true;

            //var positiveIntRegEx = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z]*$");

            //if (positiveIntRegEx.IsMatch(input) == false)
            //{
            //    pass = false;
            //}

            if (input.Trim().Length < 1)
            {
                pass = false;
            }

            return pass;
        }
    }
}