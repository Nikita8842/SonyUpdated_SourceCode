using System;
using System.Text;
using System.Security.Cryptography;

namespace AmboUtilities.Helper
{
    /// <summary>
    /// Class to handle all Encryption
    /// and Decryption as define within
    /// AMBO Product
    /// </summary>
    public static class AMBOEcryption
    {
        /// <summary>
        /// Method to Hash the Input String
        /// as within AMBO Product
        /// </summary>
        /// <param name="TextValue"></param>
        /// <returns></returns>
        public static String GetHashValue(string TextValue)
        {
            MD5 md5Encyption = MD5.Create();
            byte[] encodedText = md5Encyption.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(TextValue));

            return GetHexValue(md5Encyption.ComputeHash(encodedText));
        }

        /// <summary>
        /// hashing helper class
        /// as within AMBO Product
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string GetHexValue(byte[] text)
        {
            char[] hexChars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7',
                                           '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'
                                         };
            System.Text.StringBuilder sb = new StringBuilder();

            for (int i = 0; i < text.Length; i++)
            {
                sb.Append(hexChars[text[i] >> 4]);
                sb.Append(hexChars[text[i] & 0x0F]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Ecryption Code which written within AMBO Product
        /// </summary>
        /// <param name="toEncrypt"></param>
        /// <param name="IsTrue"></param>
        /// <returns></returns>
        public static string EncryptData(string toEncrypt, bool IsTrue)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);


            //this value are getting from WebConfig file previoulsy. 
            string key = "VFConnect";

            if (IsTrue)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();

            byte[] resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);
            tdes.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }


        /// <summary>
        /// Data Decryption which written within AMBO Product
        /// </summary>
        /// <param name="cipherString"></param>
        /// <param name="IsTure"></param>
        /// <returns></returns>
        public static string DecryptData(string cipherString, bool IsTure)
        {
            try
            {
                byte[] keyArray;
                //get the byte code of the string

                byte[] toEncryptArray = Convert.FromBase64String(cipherString);

                //System.Configuration.AppSettingsReader settingsReader =
                //                                    new AppSettingsReader();
                ////Get your key from config file to open the lock!
                //string key = (string)settingsReader.GetValue("EncryptKey",typeof(String));
                //this value are getting from WebConfig file previoulsy. 
                string key = "VFConnect";

                if (IsTure)
                {
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                   
                    hashmd5.Clear();
                }
                else
                {
                    keyArray = UTF8Encoding.UTF8.GetBytes(key);
                }

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                
                tdes.Key = keyArray;                
                tdes.Mode = CipherMode.ECB;                
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(
                                     toEncryptArray, 0, toEncryptArray.Length);
                            
                tdes.Clear();
              
                return UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

    }
}
