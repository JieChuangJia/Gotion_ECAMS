using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
namespace LicenceManager
{
    [Serializable]
    public class LicenceModel
    {
        [NonSerialized]
        private static string keyStr = "zzal12k5";//秘钥
        [NonSerialized]
        private static string vector = "80hjm76a";//向量
        private string filePath = "";
        public LicenceModel(string filePath)
        {
            this.filePath = filePath;
        }

        public string LastRunTime { get; set; }
        public string LicenceEndTime { get; set; }
        public string Reserve { get; set; }


        public string Encrypt(string sourceString)
        {
            try
            {
                byte[] btKey = Encoding.UTF8.GetBytes(keyStr);

                byte[] btIV = Encoding.UTF8.GetBytes(vector);

                DESCryptoServiceProvider des = new DESCryptoServiceProvider();

                using (MemoryStream ms = new MemoryStream())
                {
                    byte[] inData = Encoding.UTF8.GetBytes(sourceString);
                    try
                    {
                        using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(btKey, btIV), CryptoStreamMode.Write))
                        {
                            cs.Write(inData, 0, inData.Length);

                            cs.FlushFinalBlock();
                        }

                        return Convert.ToBase64String(ms.ToArray());
                    }
                    catch
                    {
                        return sourceString;
                    }
                }
            }
            catch { }

            return "DES加密出错";
        }

        public string Decrypt(string encryptedString)
        {
            byte[] btKey = Encoding.UTF8.GetBytes(keyStr);

            byte[] btIV = Encoding.UTF8.GetBytes(vector);

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            using (MemoryStream ms = new MemoryStream())
            {
                byte[] inData = Convert.FromBase64String(encryptedString);
                try
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(btKey, btIV), CryptoStreamMode.Write))
                    {
                        cs.Write(inData, 0, inData.Length);

                        cs.FlushFinalBlock();
                    }

                    return Encoding.UTF8.GetString(ms.ToArray());
                }
                catch
                {
                    return encryptedString;
                }
            }
        }

        public bool CreateLicenceFile(string filePathName)
        {
            try
            {
                Stream fStream = new FileStream(filePathName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                BinaryFormatter binFormat = new BinaryFormatter();//创建二进制序列化器
                binFormat.Serialize(fStream,this);
                fStream.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool WriteLicenceEndTime(string desStr)
        {
            try
            {
                LicenceModel licenceModel = LoadLicence();
                Stream fStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                BinaryFormatter binFormat = new BinaryFormatter();//创建二进制序列化器
                licenceModel.LicenceEndTime = desStr;
                binFormat.Serialize(fStream, licenceModel);
                fStream.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public LicenceModel LoadLicence()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    //Stream fStream1 = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    //BinaryFormatter binFormat1 = new BinaryFormatter();//创建二进制序列化器
                    //this.LastRunTime = Encrypt(DateTime.Now.ToString("yyyy-MM-dd HH:00:00"));
                    //binFormat1.Serialize(fStream1, this);
                    //fStream1.Close();
                    return null;
                }
           
                Stream fStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                BinaryFormatter binFormat = new BinaryFormatter();//创建二进制序列化器
                LicenceModel licenceModel = (LicenceModel)binFormat.Deserialize(fStream);
                licenceModel.filePath = this.filePath;
                fStream.Close();

                return licenceModel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public LicenceModel LoadLicence(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    //Stream fStream1 = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    //BinaryFormatter binFormat1 = new BinaryFormatter();//创建二进制序列化器
                    //this.LastRunTime = Encrypt(DateTime.Now.ToString("yyyy-MM-dd HH:00:00"));
                    //binFormat1.Serialize(fStream1, this);
                    //fStream1.Close();
                    return null;
                }

                Stream fStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                BinaryFormatter binFormat = new BinaryFormatter();//创建二进制序列化器
                LicenceModel licenceModel = (LicenceModel)binFormat.Deserialize(fStream);
                licenceModel.filePath = this.filePath;
                fStream.Close();

                return licenceModel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool WriteLastRunTime()
        {
            try
            {
                LicenceModel licenceModel = LoadLicence();
                Stream fStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                BinaryFormatter binFormat = new BinaryFormatter();//创建二进制序列化器
                licenceModel.LastRunTime = Encrypt(DateTime.Now.ToString("yyyy-MM-dd HH:00:00"));
                binFormat.Serialize(fStream, licenceModel);
                fStream.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool IsLicenceValid(ref string reStr)
        {
            try
            {
                LicenceModel licenceModel = LoadLicence();
                DateTime nowTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:00:00"));
                DateTime lastRunTime = DateTime.Parse(Decrypt(licenceModel.LastRunTime));
                DateTime licenceEndTime = DateTime.Parse(Decrypt(licenceModel.LicenceEndTime));
                if (nowTime < lastRunTime)
                {
                    reStr = "系统时钟错误！";
                    return false;
                }
                DateTime deadTime = licenceEndTime;
               
                TimeSpan ss = new TimeSpan(7, 0, 0, 0);
                DateTime firstLockTime = deadTime - ss;

                if (nowTime > firstLockTime)
                {
                    reStr = "期限已到:" + firstLockTime.ToLocalTime();
                    return false;
                }

                reStr = "OK";
                return true;

            }
            catch (Exception ex)
            {
                reStr = ex.ToString();
                return false;
            }
        }
    }
}
