namespace YouTubeScrap.Core.ReverseEngineer.Cipher.Operations
{
    public interface ICipherOperation
    {
        string Decipher(string cipherSignature);
    }
}