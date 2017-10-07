using System;
using Microsoft.EntityFrameworkCore.Storage;

namespace Microsoft.EntityFrameworkCore.Infrastructure
{
    public class DocumentDbTransaction : IDbContextTransaction
    {
        public Guid TransactionId => Guid.NewGuid();

        public void Commit()
        {
        }

        public void Dispose()
        {
        }

        public void Rollback()
        {
        }
    }
}
