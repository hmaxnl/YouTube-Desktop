using System.Threading.Tasks;

namespace App.Models
{
    public class Session
    {
        //TODO: Add params to set which API call to make.
        public Session(Workspace workspace)
        {
            _workspace = workspace;
            Task.Run(Init);
        }
        
        //TODO: Implement snippets

        private readonly Workspace _workspace;

        private async Task Init()
        {
            //TODO: Initialize user if not done already.
        }
    }
}