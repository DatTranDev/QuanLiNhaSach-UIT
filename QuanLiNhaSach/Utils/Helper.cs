using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Collections;

namespace QuanLiNhaSach.Utils
{
    public class Helper
    {
        public static string MD5Hash(string password)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password));
            //get hash result after compute it  
            byte[] result = md5.Hash;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }
            return strBuilder.ToString();
        }
        public static string randomCode()
        {
            string code = "";
            string inital = "QWERTYUIOPASDFGHJKLZXCVBNM123456789";
            Random rand = new Random();
            for (int i = 0; i < 6; i++)
            {
                code = code.Insert(i, inital[rand.Next(0, inital.Length - 1)].ToString());
            }
            return code;
        }
    }
}
