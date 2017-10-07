using System.Linq;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Microsoft.EntityFrameworkCore.Metadata
{
    public class DocumentDbModelCustomizer : ModelCustomizer
    {
        public DocumentDbModelCustomizer([NotNull] ModelCustomizerDependencies dependencies)
            : base(dependencies)
        {
        }

        protected override void FindSets([NotNull] ModelBuilder modelBuilder, [NotNull] DbContext context)
        {
            base.FindSets(modelBuilder, context);

            var sets = Dependencies.SetFinder.CreateClrTypeDbSetMapping(context);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes().Cast<EntityType>())
            {
                if (entityType.BaseType == null
                    && sets.ContainsKey(entityType.ClrType))
                {
                    entityType.Builder.DocumentDb(ConfigurationSource.Convention).ToCollection(sets[entityType.ClrType].Name);
                }
            }
        }
    }
}
