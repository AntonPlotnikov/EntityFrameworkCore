namespace Microsoft.EntityFrameworkCore.Metadata
{
    public class DocumentDbIndexAnnotations : IDocumentDbIndexAnnotations
    {
        public DocumentDbIndexAnnotations(IIndex index)
            : this(new DocumentDbAnnotations(index))
        {
        }

        protected DocumentDbIndexAnnotations(DocumentDbAnnotations annotations)
        {
            Annotations = annotations;
        }

        protected virtual DocumentDbAnnotations Annotations { get; }
        protected virtual IIndex Index => (IIndex)Annotations.Metadata;
    }
}
