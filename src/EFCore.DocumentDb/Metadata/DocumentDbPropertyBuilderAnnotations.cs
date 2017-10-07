using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Microsoft.EntityFrameworkCore.Metadata
{
    public class DocumentDbPropertyBuilderAnnotations : DocumentDbPropertyAnnotations
    {
        public DocumentDbPropertyBuilderAnnotations(
            InternalPropertyBuilder internalBuilder,
            ConfigurationSource configurationSource)
            : base(new DocumentDbAnnotationsBuilder(internalBuilder, configurationSource))
        {
        }
    }
}
