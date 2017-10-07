using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Microsoft.EntityFrameworkCore.Metadata
{
    public class DocumentDbIndexBuilderAnnotations : DocumentDbIndexAnnotations
    {
        public DocumentDbIndexBuilderAnnotations(
            InternalIndexBuilder internalBuilder,
            ConfigurationSource configurationSource)
            : base(new DocumentDbAnnotationsBuilder(internalBuilder, configurationSource))
        {
        }
    }
}
