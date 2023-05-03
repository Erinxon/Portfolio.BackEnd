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
        public static SqlDbType? GetSqlDbType(Type type) => type.FullName switch
        {
            string FullNameType when FullNameType.Contains(typeof(int).Name) => SqlDbType.Int,
            string FullNameType when FullNameType.Contains(typeof(long).Name) => SqlDbType.BigInt,
            string FullNameType when FullNameType.Contains(typeof(string).Name) => SqlDbType.VarChar,
            string FullNameType when FullNameType.Contains(typeof(DateTime).Name) => SqlDbType.DateTime,
            string FullNameType when FullNameType.Contains(typeof(decimal).Name) => SqlDbType.Decimal,
            string FullNameType when FullNameType.Contains(typeof(double).Name) => SqlDbType.Float,
            string FullNameType when FullNameType.Contains(typeof(bool).Name) => SqlDbType.Bit,
            string FullNameType when FullNameType.Contains(typeof(byte[]).Name) => SqlDbType.VarBinary,
            string FullNameType when FullNameType.Contains(typeof(Guid).Name) => SqlDbType.UniqueIdentifier,
            string FullNameType when FullNameType.Contains(typeof(TimeSpan).Name) => SqlDbType.Time,
            _ => null
        };

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
