using Microsoft.EntityFrameworkCore.Storage;

namespace Microsoft.EntityFrameworkCore.Query
{
    public interface IShaper<out T>
    {
        T Shape(QueryContext queryContext, ValueBuffer valueBuffer);
    }
}
