using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Microsoft.EntityFrameworkCore.Metadata
{
    public class DocumentDbKeyBuilderAnnotations : DocumentDbKeyAnnotations
    {
        public DocumentDbKeyBuilderAnnotations(
            InternalKeyBuilder internalBuilder,
            ConfigurationSource configurationSource)
            : base(new DocumentDbAnnotationsBuilder(internalBuilder, configurationSource))
        {
        }
    }
}
