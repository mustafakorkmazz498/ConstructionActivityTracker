using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Security;

public static class HashingHelper
{
    public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using HMACSHA512 hMACSHA = new HMACSHA512();
        passwordSalt = hMACSHA.Key;
        passwordHash = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using HMACSHA512 hMACSHA = new HMACSHA512(passwordSalt);
        return hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(password)).SequenceEqual(passwordHash);
    }
}
