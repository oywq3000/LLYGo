using System;
using System.Security.Cryptography;
using System.Text;

namespace Script
{
    public static class DataSecurity
    {
        //secret key
        private static string _key = "12345678123456781234567812345678";
        /// <summary>
        /// encrypt string
        /// </summary>
        /// <param name="toE"></param>
        /// <returns></returns>
        public static string EnCrypt(String toE)
        {
            //convert secret key to byte array
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(_key);
            
            //create object of RijndaelManaged
            RijndaelManaged rijndaelManaged = new RijndaelManaged();

            //configuring 
            rijndaelManaged.Key = keyArray;
            rijndaelManaged.Mode = CipherMode.ECB;
            rijndaelManaged.Padding = PaddingMode.PKCS7;
           
            
            //create encryptor
            ICryptoTransform cryptoTransform = rijndaelManaged.CreateEncryptor();

            //take would-be encrypted data to byte array
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toE);
            
            //encrypt 
            var transformFinalBlock = cryptoTransform.TransformFinalBlock(toEncryptArray,0,toEncryptArray.Length);
            
            //convert this transformFinalBlock to ToBase64String
            return Convert.ToBase64String(transformFinalBlock, 0, toEncryptArray.Length);
        }

        /// <summary>
        /// decrypt string
        /// </summary>
        /// <param name="toE"></param>
        /// <returns></returns>
        public static string DeCrypt(String toD)
        {
            //convert secret key to byte array
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(_key);
            
            //create object of RijndaelManaged
            RijndaelManaged rijndaelManaged = new RijndaelManaged();

            //configuring 
            rijndaelManaged.Key = keyArray;
            rijndaelManaged.Mode = CipherMode.ECB;
            rijndaelManaged.Padding = PaddingMode.PKCS7;
           
            
            //create encryptor
            ICryptoTransform cryptoTransform = rijndaelManaged.CreateDecryptor();
            
            //take secreted string to byte array
            var fromBase64String = Convert.FromBase64String(toD);
            
            //decrypt
            var transformFinalBlock = cryptoTransform.TransformFinalBlock(fromBase64String, 0, fromBase64String.Length);

            //take plaintext to string and return
            return UTF8Encoding.UTF8.GetString(transformFinalBlock);
        }
    }
}