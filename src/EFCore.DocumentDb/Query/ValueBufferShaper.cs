using System;
using Microsoft.EntityFrameworkCore.Storage;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class ValueBufferShaper : Shaper, IShaper<ValueBuffer>
    {
        public ValueBuffer Shape(QueryContext queryContext, ValueBuffer valueBuffer)
        {
            return valueBuffer;
        }

        public override Type Type => typeof(ValueBuffer);
    }
}
