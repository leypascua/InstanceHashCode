using System;
using System.Collections.Generic;
using System.Text;

namespace InstanceHashCode.Tests.Fixtures
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public IList<Attachment> Attachments { get; set; }
    }
}
