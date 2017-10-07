namespace Microsoft.EntityFrameworkCore.Metadata
{
    public interface IDocumentDbEntityTypeAnnotations
    {
        string CollectionName { get; }
        IProperty DiscriminatorProperty { get; }
        object DiscriminatorValue { get; }
    }
}
