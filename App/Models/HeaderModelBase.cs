namespace App.Models
{
    // Model for storing header data to the tab control.
    public class HeaderModelBase
    {
        public string Title { get; set; } = "YouTube";
        public bool IsPlaying { get; set; } = false;
    }
}