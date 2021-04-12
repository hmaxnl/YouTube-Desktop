using System;
using System.Collections.Generic;
using System.Text;

namespace YouTubeScrap.Core.ReverseEngineer.Cipher.Operations
{
    public interface ICipherOperation
    {
        string Decipher(string cipherSignature);
    }
}