using Microsoft.EntityFrameworkCore.Metadata;

namespace Microsoft.EntityFrameworkCore.Infrastructure
{
    public interface IDocumentCollectionServiceFactory
    {
        IDocumentCollectionService Create(IDocumentDbClientService documentDbClientService, IEntityType entityType);
    }
}
