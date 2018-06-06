using InstanceHashCode.Tests.Fixtures;
using System;
using System.Linq;
using System.Extensions;
using Xunit;

namespace InstanceHashCode.Tests
{
    public class ReflectionHashCodeBuilderTests
    {
        [Fact]
        public void DemarcatedPropertiesTest()
        {
            Type subjectType = typeof(Attachment);
            ReflectionHashCodeBuilder builder = HashCodeBuilderFactory.Default.CreateInstance(subjectType) as ReflectionHashCodeBuilder;
            var targetedProperties = HashCodeParameterAttribute.GetDemarcatedProperties(subjectType);

            Assert.Equal(targetedProperties.Count(), builder.TargetProperties.Count());
        }

        [Fact]
        public void BuildHashCodeTest()
        {
            var subject = new Attachment
            {
                FileHash = Guid.NewGuid().ToString().ToSha256Hash(),
                Metadata = new Metadata
                {
                    Key = "key",
                    Value = "value"
                }
            };

            ReflectionHashCodeBuilder builder = HashCodeBuilderFactory.Default.CreateInstance(subject.GetType()) as ReflectionHashCodeBuilder;

            Assert.NotNull(builder.BuildSha256Hash(subject));
        }
    }
}
