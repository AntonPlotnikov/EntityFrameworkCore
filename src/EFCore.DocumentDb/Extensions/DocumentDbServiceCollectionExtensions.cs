// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Utilities;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace Microsoft.EntityFrameworkCore.Extensions
{
    public static class DocumentDbServiceCollectionExtensions
    {
        public static IServiceCollection AddEntityFrameworkDocumentDb([NotNull] this IServiceCollection serviceCollection)
        {
            Check.NotNull(serviceCollection, nameof(serviceCollection));

            var builder = new EntityFrameworkServicesBuilder(serviceCollection)
                .TryAdd<IDatabaseProvider, DatabaseProvider<DocumentDbOptionsExtension>>()
                .TryAdd<IQueryContextFactory, DocumentDbQueryContextFactory>()
                .TryAdd<IDatabase, DocumentDbDatabase>()
                .TryAdd<IEntityQueryModelVisitorFactory, DocumentDbEntityQueryModelVisitorFactory>()
                .TryAdd<IDatabaseCreator, DocumentDbDatabaseCreator>()
                .TryAdd<IEntityQueryableExpressionVisitorFactory, DocumentDbEntityQueryableExpressionVisitorFactory>()
                .TryAdd<IExpressionPrinter, DocumentDbPrinter>()
                .TryAdd<IDbContextTransactionManager, DocumentDbTransactionManager>()
                .TryAdd<IConventionSetBuilder, DocumentDbConventionSetBuilder>()
                .TryAdd<IModelCustomizer, DocumentDbModelCustomizer>()
                //.TryAdd<IDatabase, DocumentDbDatabase>()
                //.TryAdd<IDatabaseCreator, DocumentDbDatabaseCreator>()
                //.TryAdd<IQueryContextFactory, DocumentDbQueryContextFactory>()
                //.TryAdd<IEntityQueryModelVisitorFactory, DocumentDbQueryModelVisitorFactory>()
                //.TryAdd<IEntityQueryableExpressionVisitorFactory, DocumentDbEntityQueryableExpressionVisitorFactory>()
                //.TryAdd<IEvaluatableExpressionFilter, EvaluatableExpressionFilter>()
                //.TryAdd<ISingletonOptions, IInMemorySingletonOptions>(p => p.GetService<IInMemorySingletonOptions>())
                .TryAddProviderSpecificServices(
                    b => b
                    .TryAddScoped<IDocumentDbClientService, DocumentDbClientService>()
                    .TryAddScoped<ISqlTranslatingExpressionVisitorFactory, SqlTranslatingExpressionVisitorFactory>()
                        //        .TryAddSingleton<IInMemorySingletonOptions, InMemorySingletonOptions>()
                        //        .TryAddSingleton<IInMemoryStoreCache, InMemoryStoreCache>()
                        //        .TryAddSingleton<IInMemoryTableFactory, InMemoryTableFactory>()
                        //.TryAddScoped<IDocumentDbClient, DocumentDbClient>()
                        //.TryAddScoped<IMaterializerFactory, MaterializerFactory>()
                        .TryAddScoped<IDocumentCollectionServiceFactory, DocumentCollectionServiceFactory>()
                        )
                ;
            builder.TryAddCoreServices();

            return serviceCollection;
        }
    }
}
