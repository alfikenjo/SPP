using FluentFTP;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Frontend_SPP.Common
{
    public class PassHash
    {
        private static readonly RandomNumberGenerator Rng = RandomNumberGenerator.Create();

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        private static bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (a == null && b == null) return true;
            if (a == null || b == null || a.Length != b.Length) return false;

            var flag = true;
            for (var index = 0; index < a.Length; ++index) flag &= a[index] == b[index];

            return flag;
        }

        private static byte[] HashPasswordV2(string password, RandomNumberGenerator rng)
        {
            var numArray1 = new byte[16];
            rng.GetBytes(numArray1);

            var k1 = new Rfc2898DeriveBytes(password, numArray1, 1000);
            var numArray2 = k1.GetBytes(32);
            var numArray3 = new byte[49];
            numArray3[0] = 0;

            Buffer.BlockCopy(numArray1, 0, numArray3, 1, 16);

            const int srcOffset = 0;
            const int dstOffset = 17;
            const int count = 32;
            var numArray4 = numArray3;

            Buffer.BlockCopy(numArray2, srcOffset, numArray4, dstOffset, count);

            return numArray3;
        }

        private static bool VerifyHashedPasswordV2(byte[] hashedPassword, string password)
        {
            if (hashedPassword.Length != 49) return false;
            var salt = new byte[16];

            Buffer.BlockCopy(hashedPassword, 1, salt, 0, salt.Length);

            var b = new byte[32];
            Buffer.BlockCopy(hashedPassword, 1 + salt.Length, b, 0, b.Length);

            var k1 = new Rfc2898DeriveBytes(password, salt, 1000);
            var numArray2 = k1.GetBytes(32);

            return ByteArraysEqual(numArray2, b);
        }

        /// <summary>
        /// Returns a hashed representation of the supplied <paramref name="password" />.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <returns>A hashed representation of the supplied <paramref name="password" />.</returns>
        public static string HashPassword(string password)
        {
            if (password == null) throw new ArgumentNullException(password);
            return Convert.ToBase64String(HashPasswordV2(password, Rng));
        }

        public static bool VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            if (hashedPassword == null)
                throw new ArgumentNullException(hashedPassword);
            if (providedPassword == null)
                throw new ArgumentNullException(providedPassword);

            var hashedPassword1 = Convert.FromBase64String(hashedPassword);
            if (hashedPassword1.Length == 0) return false;

            if (hashedPassword1[0] != 0) return false;
            if (!VerifyHashedPasswordV2(hashedPassword1, providedPassword)) return false;

            return true;
        }
    }
}
