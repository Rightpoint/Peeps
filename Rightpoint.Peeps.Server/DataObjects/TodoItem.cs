using Microsoft.WindowsAzure.Mobile.Service;

namespace Rightpoint.Peeps.Server.DataObjects
{
    public class TodoItem : EntityData
    {
        public string Text { get; set; }

        public bool Complete { get; set; }
    }
}