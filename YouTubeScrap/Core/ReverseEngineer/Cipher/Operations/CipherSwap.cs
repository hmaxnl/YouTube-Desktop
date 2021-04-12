using System;
using System.Collections.Generic;
using System.Text;

namespace YouTubeScrap.Core.ReverseEngineer.Cipher.Operations
{
    public class CipherSwap : ICipherOperation
    {
        private readonly int _index;
        public CipherSwap(int indexToSwap) => _index = indexToSwap;
        public string Decipher(string cipherSignature) => new StringBuilder(cipherSignature)
        {
            [0] = cipherSignature[_index],
            [_index] = cipherSignature[0]
        }.ToString();
    }
}