using System;
using System.Collections.Generic;
using System.Text;

namespace InstanceHashCode.Tests.Fixtures
{
    public class Attachment
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public DateTime CreatedAt { get; set; }

        [HashCodeParameter]
        public string FileHash { get; set; }

        [HashCodeParameter]
        public Metadata Metadata { get; set; }
    }
}