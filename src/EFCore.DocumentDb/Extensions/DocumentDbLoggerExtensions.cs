using JetBrains.Annotations;
using Microsoft.Azure.Documents;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Microsoft.EntityFrameworkCore.Extensions
{
    public static class DocumentDbLoggerExtensions
    {
        public static void CommandExecuting(
            [NotNull] this IDiagnosticsLogger<DbLoggerCategory.Database.Command> diagnostics,
            SqlQuerySpec querySpec)
        {
            diagnostics.Logger.LogInformation(CoreEventId.ProviderBaseId, querySpec.QueryText);
        }
    }
}
