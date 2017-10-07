using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.EntityFrameworkCore.Update;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.EntityFrameworkCore.Infrastructure
{
    public interface IDocumentDbClientService
    {
        DocumentClient Client { get; }
        string DatabaseId { get; }

        IEnumerator<Document> ExecuteQuery(
            string collectionId,
            SqlQuerySpec sqlQuerySpec);
        Task<int> SaveChangesAsync(IReadOnlyList<IUpdateEntry> entries, CancellationToken cancellationToken);
    }
}
