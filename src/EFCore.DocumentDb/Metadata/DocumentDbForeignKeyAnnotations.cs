namespace Microsoft.EntityFrameworkCore.Metadata
{
    public class DocumentDbForeignKeyAnnotations : IDocumentDbForeignKeyAnnotations
    {
        public DocumentDbForeignKeyAnnotations(IForeignKey foreignKey)
            : this(new DocumentDbAnnotations(foreignKey))
        {
        }

        protected DocumentDbForeignKeyAnnotations(DocumentDbAnnotations annotations)
        {
            Annotations = annotations;
        }

        protected virtual DocumentDbAnnotations Annotations { get; }
        protected virtual IForeignKey ForeignKey => (IForeignKey)Annotations.Metadata;
    }
}
