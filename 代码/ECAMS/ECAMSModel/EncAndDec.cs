
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace ECAMSModel
{
    public class EncAndDec
    {
        //const string KEY_64 = "zwx";//注意了，是8个字符，64位    

        //const string IV_64 = "87654321";

        #region DES加密的方法
        /// <summary>
        /// 加密的方法，通过2个密匙进行加密
        /// </summary>
        /// <param name="data">通过Md5加密一次</param>
        /// <param name="KEY_64"></param>
        /// <param name="IV_64"></param>
        /// <returns></returns>
        public static string Encode(string data, string KEY_64, string IV_64)
        {
            try
            {
                KEY_64 = ToMD5(KEY_64);
                IV_64 = ToMD5(IV_64);
                byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);//用于对称算法的密钥
                byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV_64);//用于对称算法的初始化向量

                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                int i = cryptoProvider.KeySize;
                MemoryStream ms = new MemoryStream();
                CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);

                StreamWriter sw = new StreamWriter(cst);
                sw.Write(data);
                sw.Flush();
                cst.FlushFinalBlock();
                sw.Flush();
                return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
            }
            catch (System.Exception ex)
            {
                return null;
            }


        }
        /// <summary>
        /// 解密的方法（）
        /// </summary>
        /// <param name="data"></param>
        /// <param name="KEY_64"></param>
        /// <param name="IV_64"></param>
        /// <returns></returns>
        public static string Decode(string data, string KEY_64, string IV_64)
        {
            try
            {
                KEY_64 = ToMD5(KEY_64);
                IV_64 = ToMD5(IV_64);
                byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);
                byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV_64);

                byte[] byEnc;

                byEnc = Convert.FromBase64String(data);


                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                MemoryStream ms = new MemoryStream(byEnc);
                CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);
                StreamReader sr = new StreamReader(cst);
                return sr.ReadToEnd();
            }
            catch (System.Exception ex)
            {
                return null;
            }

        }
        #endregion

        #region MD5加密
        /// <summary>
        /// 转换MD5密码
        /// </summary>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static string ToMD5(string KEY)
        {
            byte[] result = Encoding.Default.GetBytes(KEY);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);

            string KEY_64 = BitConverter.ToString(output).Replace("-", "").Substring(0, 8);
            return KEY_64;

        }
        #endregion

    }
}
