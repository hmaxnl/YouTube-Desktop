namespace YouTubeScrap.Core.ReverseEngineer.Cipher.Operations
{
    public class CipherSlice : ICipherOperation
    {
        private readonly int _index;
        public CipherSlice(int indexToSlice) => _index = indexToSlice;
        public string Decipher(string cipherSignature) => cipherSignature.Substring(_index);
    }
}