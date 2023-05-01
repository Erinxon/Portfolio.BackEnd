using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tools
{
    public static class Utility
    {
        public static SqlDbType GetSqlDbType(object value)
        {
            var type = value?.GetType();
            return type?.Name switch
            {
                "String" => SqlDbType.VarChar,
                "Int32" => SqlDbType.Int,
                "DateTime" => SqlDbType.DateTime,
                "Guid" => SqlDbType.UniqueIdentifier,
                "Long" => SqlDbType.BigInt,
                _ => SqlDbType.VarChar
            };
        }

        public static Dictionary<string, object> ToDictionary(object obj)
        {
            int i = 0;
            var props = obj.GetType().GetProperties();
            return props.ToDictionary(k => props[i].Name, v => props[i++].GetValue(obj));
        }

        public static string ToEncryptedPassword(this string Password)
        {
            SHA256 sha256 = SHA256.Create();
            ASCIIEncoding encoding = new();
            byte[] stream = null;
            StringBuilder sb = new();
            stream = sha256.ComputeHash(encoding.GetBytes(Password));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

    }
}
