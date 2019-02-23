
namespace FileShareSite.Models
{
    public class ErrorViewModel
    {
        public string RequestIdentifier { get; }
        public bool ShowRequestIdentifier => !string.IsNullOrEmpty(RequestIdentifier);

        public ErrorViewModel(string identifier)
        {
            RequestIdentifier = identifier;
        }
    }
}