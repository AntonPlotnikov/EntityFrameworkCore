using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Microsoft.EntityFrameworkCore.Metadata
{
    public class DocumentDbModelBuilderAnnotations : DocumentDbModelAnnotations
    {
        public DocumentDbModelBuilderAnnotations(
            InternalModelBuilder internalBuilder,
            ConfigurationSource configurationSource)
            : base(new DocumentDbAnnotationsBuilder(internalBuilder, configurationSource))
        {
        }
    }
}
