namespace App.Models
{
    // Interface for the header.
    public interface IHeaderModel
    {
        public string Title { get; set; }
        public bool IsPlaying { get; set; }
    }
}