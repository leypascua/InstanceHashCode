using InstanceHashCode.Tests.Fixtures;
using System;
using System.Collections.Generic;
using System.Extensions;
using Xunit;

namespace InstanceHashCode.Tests
{
    public class CalculateSha256HashTests
    {
        [Fact]
        public void WithDemarcatedPropertiesTest()
        {
            string fileHash = Guid.NewGuid().GetSha256HashCode();
            string key = "key";
            string value = "value";

            var subject = new Attachment
            {
                Name = "mama",
                CreatedAt = DateTime.UtcNow,
                Size = 69,

                // FileHash and Metadata are demarcated with [HashCodeParameter]
                FileHash = fileHash,
                Metadata = new Metadata
                {
                    Key = key,
                    Value = value
                }
            };

            string expected = Helpers.BuildSha256Hash(fileHash, key, value);
            string result = subject.GetSha256HashCode();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void WithNullDemarcatedPropertyTest()
        {
            var subject = new Attachment
            {
                Name = "mama",
                CreatedAt = DateTime.UtcNow,
                Size = 69,

                // FileHash and Metadata are demarcated with [HashCodeParameter]
                FileHash = Guid.NewGuid().GetMd5HashCode(),
                Metadata = null
            };

            string expected = Helpers.BuildSha256Hash(subject.FileHash);
            string result = subject.GetSha256HashCode();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void WithNullArrayTest()
        {
            var subject = new Message
            {
                Id = 1,
                Text = "Hello World",                
            };

            string expected = Helpers.BuildSha256Hash(subject.Id.ToString(), subject.Text);

            string result = subject.GetSha256HashCode();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void WithEmptyArrayTest()
        {
            var subject = new Message
            {
                Id = 1,
                Text = "Hello World",
                Attachments = new List<Attachment>()
            };

            string expected = Helpers.BuildSha256Hash(subject.Id.ToString(), subject.Text);

            string result = subject.GetSha256HashCode();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void WithArrayContentsTest()
        {
            var subject = new Message
            {
                Id = 1,
                Text = "Hello World",
                Attachments = new List<Attachment>
                {
                    new Attachment { FileHash = Guid.NewGuid().ToString() },
                    new Attachment { FileHash = Guid.NewGuid().ToString() }
                }
            };

            string expected = Helpers.BuildSha256Hash(
                subject.Id.ToString(), 
                subject.Text,
                subject.Attachments[0].FileHash,
                subject.Attachments[1].FileHash);

            string result = subject.GetSha256HashCode();

            Assert.Equal(expected, result);
        }
    }
}
