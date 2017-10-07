namespace Microsoft.EntityFrameworkCore.Metadata
{
    public class DocumentDbModelAnnotations : IDocumentDbModelAnnotations
    {
        public DocumentDbModelAnnotations(IModel model)
            : this(new DocumentDbAnnotations(model))
        {
        }

        protected DocumentDbModelAnnotations(DocumentDbAnnotations annotations)
        {
            Annotations = annotations;
        }

        protected virtual DocumentDbAnnotations Annotations { get; }
        protected virtual IModel Model => (IModel)Annotations.Metadata;
    }
}
