using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.SFAMobileApp
{
    public class SFAMessageBroadcasterModel
    {
        public Int64 BroadcastMessageId { get; set; }
        public Int64 SFAId { get; set; }
        public string Reply { get; set; }
        public string FileExtention { get; set; }
        public string File { get; set; }
    }

    public class MessageBroadcasterModel
    {
        public string BroadcastMessageId { get; set; }
        public string Message { get; set; }
        public string FileName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
    }

    public class MessageBroadcasterList
    {
        public List<MessageBroadcasterModel> Messages { get; set; }
        public MessageBroadcasterList()
        {
            Messages = new List<MessageBroadcasterModel>();
        }
    }

    public class MessageListProcInput
    {
        public Int64 UserId { get; set; }
    }
}
