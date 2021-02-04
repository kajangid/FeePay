using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.UseCase
{
    internal static class Crypto
    {
        static string tbKey = "a1b2c3d4e5";

        public static string TripleDESEncrypt(string tbMessage)
        {
            try
            {
                using TripleDES threedes = new TripleDESCryptoServiceProvider();
                threedes.Key = StringToByte(tbKey, 24); // convert to 24 characters - 192 bits
                threedes.IV = StringToByte("12345678");
                byte[] key = threedes.Key;
                byte[] IV = threedes.IV;

                ICryptoTransform encryptor = threedes.CreateEncryptor(key, IV);

                using MemoryStream msEncrypt = new MemoryStream();
                CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);

                // Write all data to the crypto stream and flush it.
                csEncrypt.Write(StringToByte(tbMessage), 0, StringToByte(tbMessage).Length);
                csEncrypt.FlushFinalBlock();

                // Get the encrypted array of bytes.
                byte[] encrypted = msEncrypt.ToArray();

                return ByteToString(encrypted);
            }
            catch (Exception ex)
            {
                throw new Exception("TripleDESEncrypt Stop on Error.", ex);
            }

        }

        private static string TripleDESEncryptID(int Message)
        {
            string tbMessage = Message.ToString();
            try
            {

                using TripleDES threedes = new TripleDESCryptoServiceProvider();
                threedes.Key = StringToByte(tbKey, 24); // convert to 24 characters - 192 bits
                threedes.IV = StringToByte("12345678");
                byte[] key = threedes.Key;
                byte[] IV = threedes.IV;

                ICryptoTransform encryptor = threedes.CreateEncryptor(key, IV);

                using MemoryStream msEncrypt = new MemoryStream();
                CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);

                // Write all data to the crypto stream and flush it.
                csEncrypt.Write(StringToByte(tbMessage), 0, StringToByte(tbMessage).Length);
                csEncrypt.FlushFinalBlock();

                // Get the encrypted array of bytes.
                byte[] encrypted = msEncrypt.ToArray();

                return ByteToString(encrypted);
            }
            catch (Exception ex)
            {
                throw new Exception("TripleDESEncryptID Stop on Error.", ex);
            }

        }

        private static string TripleDESDecrypt(string encryptedMessage)
        {
            try
            {
                using TripleDES threedes = new TripleDESCryptoServiceProvider();
                threedes.Key = StringToByte(tbKey, 24); // convert to 24 characters - 192 bits
                threedes.IV = StringToByte("12345678");
                byte[] key = threedes.Key;
                byte[] IV = threedes.IV;

                ICryptoTransform decryptor = threedes.CreateDecryptor(key, IV);

                // Now decrypt the previously encrypted message using the decryptor
                byte[] encrypted = StringToByteDecimal(encryptedMessage);
                using MemoryStream msDecrypt = new MemoryStream(encrypted);
                CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);

                return ByteToString(csDecrypt);
            }
            catch (Exception ex)
            {
                throw new Exception("TripleDESDecrypt Stop on Error.", ex);
            }
        }

        private static int TripleDESDecryptID(string encryptedMessage)
        {
            int decraptvalue = 0;
            try
            {
                if (encryptedMessage != "0")
                {
                    using TripleDES threedes = new TripleDESCryptoServiceProvider();
                    threedes.Key = StringToByte(tbKey, 24); // convert to 24 characters - 192 bits
                    threedes.IV = StringToByte("12345678");
                    byte[] key = threedes.Key;
                    byte[] IV = threedes.IV;

                    ICryptoTransform decryptor = threedes.CreateDecryptor(key, IV);

                    // Now decrypt the previously encrypted message using the decryptor
                    byte[] encrypted = StringToByteDecimal(encryptedMessage);
                    using MemoryStream msDecrypt = new MemoryStream(encrypted);
                    CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                    decraptvalue = int.Parse(ByteToString(csDecrypt));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("TripleDESDecryptID Stop on Error.", ex);
            }
            return decraptvalue;
        }

        private static byte[] StringToByteDecimal(string StringToConvert)
        {
            int j = 0;
            string dec = "";
            byte[] ByteArray = new byte[StringToConvert.Length / 2];
            for (int i = 0; i < StringToConvert.Length; i = i + 2)
            {
                dec = HexToDec(StringToConvert.Substring(i, 2));
                ByteArray[j] = Convert.ToByte(dec);
                j++;
            }
            return ByteArray;
        }

        private static byte[] StringToByte(string StringToConvert)
        {

            char[] CharArray = StringToConvert.ToCharArray();
            byte[] ByteArray = new byte[CharArray.Length];
            for (int i = 0; i < CharArray.Length; i++) ByteArray[i] = Convert.ToByte(CharArray[i]);
            return ByteArray;
        }
        private static byte[] StringToByte(string StringToConvert, int length)
        {

            char[] CharArray = StringToConvert.ToCharArray();
            byte[] ByteArray = new byte[length];
            for (int i = 0; i < CharArray.Length; i++) ByteArray[i] = Convert.ToByte(CharArray[i]);
            return ByteArray;
        }
        private static string ByteToString(CryptoStream buff)
        {
            string sbinary = "";
            int b = 0;
            do
            {
                b = buff.ReadByte();
                if (b != -1) sbinary += ((char)b);

            } while (b != -1);
            return (sbinary);
        }
        private static string ByteToString(byte[] buff)
        {
            string sbinary = "";
            for (int i = 0; i < buff.Length; i++) sbinary += buff[i].ToString("X2"); // hex format
            return (sbinary);
        }
        private static string HexToDec(string hexNum)
        {
            return Convert.ToString(int.Parse(hexNum, System.Globalization.NumberStyles.HexNumber));
        }
    }
}
