using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YouTubeScrap.Core.ReverseEngineer.Cipher.Operations
{
    public class CipherReverse : ICipherOperation
    {
        public string Decipher(string cipherSignature)
        {
            var buffer = new StringBuilder(cipherSignature.Length);

            for (var i = cipherSignature.Length - 1; i >= 0; i--)
                buffer.Append(cipherSignature[i]);

            return buffer.ToString();
        }
    }
}