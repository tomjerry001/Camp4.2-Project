using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CMS.Utility
{
    public class CustomValidation
    {
        public static bool IsValidUserName(string userName)
        {
            return !string.IsNullOrWhiteSpace(userName) &&
                 Regex.IsMatch(userName, @"^[a-zA-Z0-9_.]+$");
        }


        public static bool IsValidPassword(string password)
        {
            return !string.IsNullOrWhiteSpace(password) &&
                 Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{4,}$");
        }

        //Replace Alphabets with * symbol for password

        public static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                // 1 each keystroke from the user , replaces it
                // with an asterik(*)
                // and add it to the pasword string

                if (key.Key != ConsoleKey.Backspace
                    && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                // Allows the user to backspace and correct mistakes while typing password
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)

                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");

                }


            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return password;
        }
    }
}
