namespace Authorize.Application.Features.Applications.Queries.Models
{
    public class ApplicationVM
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool IsEnabled { get; private set; }

        public ApplicationVM(string name, string description, bool isEnabled)
        {
            Name = name;
            Description = description;
            IsEnabled = isEnabled;
        }
    }
}