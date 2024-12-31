using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace AmboUtilities.Helper
{
    /// <summary>
    /// Class Contain Methods which can be accessible
    /// Globally. Entire Project
    /// </summary>
    public static class GlobalAccessible
    {

        /// <summary>
        /// Method Accept String or Contactination String with ;
        /// method will split the same with ;
        /// retrun flase if found any special character
        /// else true
        ///</summary>
        /// <param name="Val"></param>
        /// <returns>ture/false</returns>
        public static bool ValidateSpecialCharacter(string Val)
        {
            //string str = "~`!@#$%^&*0987644()_+|}[{}";
            var IsValid = false;
            string[] stringArray = Val.Split(';');

            for (int i = 0; i < stringArray.Count(); i++)
            {
                var isMatched = Regex.Match(stringArray[i], "[^a-z]", RegexOptions.IgnoreCase);
                if (isMatched.Success)
                {
                    IsValid = false; break;
                }
                else
                    IsValid = true;
            }
            return IsValid;
        }

        /// BugID: 0001866
        /// <summary>
        /// Method Accept String DOJ and DOL
        /// return false if DOL less than DOJ
        /// else return true
        ///</summary>
        /// <param name="DOJ"></param>
        /// <param name="DOL"></param>
        /// <returns>true/false</returns>
        public static bool CheckJoiningAndLeavingDates(string DOJ, string DOL)
        {
            try
            {
                if (DOJ != null)
                    DOJ = DOJ.Trim();
                if (DOL != null)
                {
                    DOL = DOL.Trim();
                    if (DateTime.ParseExact(DOJ, "dd/MM/yyyy", CultureInfo.InvariantCulture) <= DateTime.ParseExact(DOL, "dd/MM/yyyy", CultureInfo.InvariantCulture))
                        return true;
                    else
                        return false;
                }
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Method Accept String DOJ and DOB
        /// return false if DOJ less than DOB
        /// else return true
        ///</summary>
        /// <param name="DOB"></param>
        /// <param name="DOJ"></param>
        /// <returns>true/false</returns>
        public static bool CheckBirthAndJoiningDates(string DOB, string DOJ)
        {
            try
            {
                if (DOB != null)
                    DOB = DOB.Trim();
                if (DOJ != null)
                {
                    DOJ = DOJ.Trim();
                    if (DateTime.ParseExact(DOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) <= DateTime.ParseExact(DOJ, "dd/MM/yyyy", CultureInfo.InvariantCulture))
                        return true;
                    else
                        return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// BugID: 0001876
        /// <summary>
        /// Method Accept String 
        /// return false if not a valid email format
        /// else return true
        ///</summary>
        /// <param name="email"></param>
        /// <returns>true/false</returns>
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }      
}
