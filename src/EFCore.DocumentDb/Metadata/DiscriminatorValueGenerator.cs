using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Microsoft.EntityFrameworkCore.Metadata
{
    public class DiscriminatorValueGenerator : ValueGenerator
    {
        private readonly object _discriminator;
        public DiscriminatorValueGenerator(object discriminator)
        {
            _discriminator = discriminator;
        }
        protected override object NextValue(EntityEntry entry) => _discriminator;
        public override bool GeneratesTemporaryValues => false;
    }
}
