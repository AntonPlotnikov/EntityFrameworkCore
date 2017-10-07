// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Utilities;

// ReSharper disable once CheckNamespace
namespace Microsoft.EntityFrameworkCore.Extensions
{
    public static class DocumentDbContextOptionsExtensions
    {
        public static DbContextOptionsBuilder<TContext> UseDocumentDb<TContext>(
            [NotNull] this DbContextOptionsBuilder<TContext> optionsBuilder,
            [NotNull] string databaseName,
            [CanBeNull] Action<DocumentDbContextOptionsBuilder> documentDbOptionsAction = null)
            where TContext : DbContext
            => (DbContextOptionsBuilder<TContext>)UseDocumentDb(
                (DbContextOptionsBuilder)optionsBuilder, databaseName, documentDbOptionsAction);

        public static DbContextOptionsBuilder UseDocumentDb(
            [NotNull] this DbContextOptionsBuilder optionsBuilder,
            [NotNull] string databaseName,
            [CanBeNull] Action<DocumentDbContextOptionsBuilder> documentDbOptionsAction = null)
        {
            Check.NotNull(optionsBuilder, nameof(optionsBuilder));
            Check.NotEmpty(databaseName, nameof(databaseName));

            var extension = optionsBuilder.Options.FindExtension<DocumentDbOptionsExtension>()
                            ?? new DocumentDbOptionsExtension();

            extension = extension.WithStoreName(databaseName);

            //// TODO: At present there is no warning to ignore.
            ////ConfigureWarnings(optionsBuilder);

            ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(extension);

            documentDbOptionsAction?.Invoke(new DocumentDbContextOptionsBuilder(optionsBuilder));

            return optionsBuilder;
        }

        //private static void ConfigureWarnings(DbContextOptionsBuilder optionsBuilder)
        //{
        //    // Set warnings defaults
        //    var coreOptionsExtension
        //        = optionsBuilder.Options.FindExtension<CoreOptionsExtension>()
        //          ?? new CoreOptionsExtension();

        //    ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(coreOptionsExtension);
        //}
    }
}
