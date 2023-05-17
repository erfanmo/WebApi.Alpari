using System;
using System.Security.Cryptography;
using System.Text;

namespace WebApi.Alpari.Helper
{
    public class SecurityHelper
    {
        private readonly RandomNumberGenerator random = RandomNumberGenerator.Create();
        public string Getsha256Hash(string value)
        {
            var algoritm = new SHA256CryptoServiceProvider();
            var bytevalue = Encoding.UTF8.GetBytes(value);
            var byteHash= algoritm.ComputeHash(bytevalue);
            return Convert.ToBase64String(byteHash);
        }

    }
}
