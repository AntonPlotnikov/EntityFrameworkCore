using System;

namespace Microsoft.EntityFrameworkCore.Query
{
    public abstract class Shaper
    {
        public abstract Type Type { get; }
    }
}
