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
        static readonly string _Key = "a1b2c3d4e5";
        static readonly string _IV = "12345678";
        static readonly byte[] _KeyInByte = StringToByte(_Key, 24); // convert to 24 characters - 192 bits
        static readonly byte[] _IVInByte = StringToByte(_IV);
        static ICryptoTransform GetEncryptor()
        {
            using TripleDES threedes = new TripleDESCryptoServiceProvider { Key = _KeyInByte, IV = _IVInByte };
            return threedes.CreateEncryptor(threedes.Key, threedes.IV);
        }
        static ICryptoTransform GetDecryptor()
        {
            using TripleDES threedes = new TripleDESCryptoServiceProvider { Key = _KeyInByte, IV = _IVInByte };
            return threedes.CreateDecryptor(threedes.Key, threedes.IV);
        }

        #region EncryptDecrypt Number
        public static string EncryptText(string input) => TripleDESEncrypt(input);
        public static string DecryptText(string input) => TripleDESDecrypt(input);

        public static string EncryptId(int input) => TripleDESEncryptInteger(input);
        public static int DecryptId(string input) => TripleDESDecryptInteger(input);
        #endregion

        #region Encrypt & Decrypt With DES Algorithm
        static string TripleDESEncrypt(string plainMessage)
        {
            if (string.IsNullOrEmpty(plainMessage)) return plainMessage;
            try
            {
                ICryptoTransform encryptor = GetEncryptor();

                using MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

                // Write all data to the cryptoStream and flush it.
                cryptoStream.Write(StringToByte(plainMessage), 0, StringToByte(plainMessage).Length);
                cryptoStream.FlushFinalBlock();

                // Get the encrypted array of bytes.
                byte[] encrypted = memoryStream.ToArray();

                return ByteToString(encrypted);
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(TripleDESEncrypt)} Stop on Error: {ex.Message}", ex);
            }

        }
        static string TripleDESEncryptInteger(int plainMessage = 0)
        {
            string plainMessageString = plainMessage.ToString();
            try
            {
                ICryptoTransform encryptor = GetEncryptor();

                using MemoryStream msEncrypt = new MemoryStream();
                CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);

                // Write all data to the crypto stream and flush it.
                csEncrypt.Write(StringToByte(plainMessageString), 0, StringToByte(plainMessageString).Length);
                csEncrypt.FlushFinalBlock();

                // Get the encrypted array of bytes.
                byte[] encrypted = msEncrypt.ToArray();

                return ByteToString(encrypted);
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(TripleDESEncryptInteger)} Stop on Error: {ex.Message}", ex);
            }

        }
        static string TripleDESDecrypt(string encryptedMessage)
        {
            if (string.IsNullOrEmpty(encryptedMessage)) return encryptedMessage;
            try
            {
                ICryptoTransform decryptor = GetDecryptor();

                // Now decrypt the previously encrypted message using the decryptor
                byte[] encrypted = StringToByteDecimal(encryptedMessage);
                using MemoryStream msDecrypt = new MemoryStream(encrypted);
                CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);

                return ByteToString(csDecrypt);
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(TripleDESDecrypt)} Stop on Error: {ex.Message}", ex);
            }
        }
        static int TripleDESDecryptInteger(string encryptedMessage)
        {
            if (string.IsNullOrEmpty(encryptedMessage)) return 0;
            try
            {
                ICryptoTransform decryptor = GetDecryptor();

                // Now decrypt the previously encrypted message using the decryptor
                byte[] encrypted = StringToByteDecimal(encryptedMessage);
                using MemoryStream msDecrypt = new MemoryStream(encrypted);
                CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                return int.Parse(ByteToString(csDecrypt));
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(TripleDESDecryptInteger)} Stop on Error: {ex.Message}", ex);
            }
        }
        static byte[] StringToByteDecimal(string StringToConvert)
        {
            int j = 0;
            string dec;
            byte[] ByteArray = new byte[StringToConvert.Length / 2];
            for (int i = 0; i < StringToConvert.Length; i = i + 2)
            {
                dec = HexToDec(StringToConvert.Substring(i, 2));
                ByteArray[j] = Convert.ToByte(dec);
                j++;
            }
            return ByteArray;
        }
        static byte[] StringToByte(string StringToConvert)
        {

            char[] CharArray = StringToConvert.ToCharArray();
            byte[] ByteArray = new byte[CharArray.Length];
            for (int i = 0; i < CharArray.Length; i++) ByteArray[i] = Convert.ToByte(CharArray[i]);
            return ByteArray;
        }
        static byte[] StringToByte(string StringToConvert, int length)
        {

            char[] CharArray = StringToConvert.ToCharArray();
            byte[] ByteArray = new byte[length];
            for (int i = 0; i < CharArray.Length; i++) ByteArray[i] = Convert.ToByte(CharArray[i]);
            return ByteArray;
        }
        static string ByteToString(CryptoStream buff)
        {
            StringBuilder sbinary = new StringBuilder();
            int b;
            do
            {
                b = buff.ReadByte();
                if (b != -1) sbinary.Append((char)b);

            } while (b != -1);
            return (sbinary.ToString());
        }
        static string ByteToString(byte[] buff)
        {
            StringBuilder sbinary = new StringBuilder();
            for (int i = 0; i < buff.Length; i++) sbinary.Append(buff[i].ToString("X2")); // hex format
            return (sbinary.ToString());
        }
        static string HexToDec(string hexNum)
        {
            return Convert.ToString(int.Parse(hexNum, System.Globalization.NumberStyles.HexNumber));
        }
        #endregion
    }
}
