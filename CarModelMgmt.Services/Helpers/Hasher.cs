using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarModelMgmt.Services.Helpers
{
    public class Hasher
    {
        //public string HashOtp(string otp)
        //{
        //    using (var argon2 = new Argon2id(Encoding.UTF8.GetBytes(otp)))
        //    {
        //        // You can adjust these parameters based on your security requirements.
        //        argon2.Iterations = 64;
        //        argon2.MemorySize = 4096;
        //        argon2.DegreeOfParallelism = 4;

        //        byte[] hashBytes = argon2.GetBytes(6);
        //        return Convert.ToBase64String(hashBytes);
        //    }
        //}

        //public string HashPassword(string password)
        //{
        //    using (var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password)))
        //    {
        //        argon2.Iterations = 64;
        //        argon2.MemorySize = 4096;
        //        argon2.DegreeOfParallelism = 4;

        //        byte[] hashBytes = argon2.GetBytes(10);
        //        return Convert.ToBase64String(hashBytes);
        //    }
        //}
    }
}
