namespace Microsoft.EntityFrameworkCore.Metadata
{
    public class DocumentDbKeyAnnotations : IDocumentDbKeyAnnotations
    {
        public DocumentDbKeyAnnotations(IKey key)
            : this(new DocumentDbAnnotations(key))
        {
        }

        protected DocumentDbKeyAnnotations(DocumentDbAnnotations annotations)
        {
            Annotations = annotations;
        }

        protected virtual DocumentDbAnnotations Annotations { get; }
        protected virtual IKey Key => (IKey)Annotations.Metadata;
    }
}
