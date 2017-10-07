using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Microsoft.EntityFrameworkCore.Metadata
{
    public class DocumentDbForeignKeyBuilderAnnotations : DocumentDbForeignKeyAnnotations
    {
        public DocumentDbForeignKeyBuilderAnnotations(
            InternalRelationshipBuilder internalBuilder,
            ConfigurationSource configurationSource)
            : base(new DocumentDbAnnotationsBuilder(internalBuilder, configurationSource))
        {
        }
    }
}
