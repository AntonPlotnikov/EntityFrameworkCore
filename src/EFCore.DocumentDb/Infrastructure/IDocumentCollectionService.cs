using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Update;

namespace Microsoft.EntityFrameworkCore.Infrastructure
{
    public interface IDocumentCollectionService
    {
        Task CreateAsync(IUpdateEntry entry);
        Task UpdateAsync(IUpdateEntry entry);
        Task DeleteAsync(IUpdateEntry entry);
    }
}
