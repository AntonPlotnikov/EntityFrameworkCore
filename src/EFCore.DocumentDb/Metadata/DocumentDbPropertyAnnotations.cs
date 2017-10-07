namespace Microsoft.EntityFrameworkCore.Metadata
{
    public class DocumentDbPropertyAnnotations : IDocumentDbPropertyAnnotations
    {
        public DocumentDbPropertyAnnotations(IProperty property)
            : this(new DocumentDbAnnotations(property))
        {
        }

        protected DocumentDbPropertyAnnotations(DocumentDbAnnotations annotations)
        {
            Annotations = annotations;
        }

        protected virtual DocumentDbAnnotations Annotations { get; }
        protected virtual IProperty Property => (IProperty)Annotations.Metadata;
    }
}
