﻿namespace Microsoft.EntityFrameworkCore.Metadata
{
    public static class DocumentDbMetadataExtensions
    {
        public static IDocumentDbModelAnnotations DocumentDb(this IModel model)
            => new DocumentDbModelAnnotations(model);
        public static IDocumentDbEntityTypeAnnotations DocumentDb(this IEntityType entityType)
            => new DocumentDbEntityTypeAnnotations(entityType);
        public static IDocumentDbPropertyAnnotations DocumentDb(this IProperty property)
            => new DocumentDbPropertyAnnotations(property);
        public static IDocumentDbIndexAnnotations DocumentDb(this IIndex index)
            => new DocumentDbIndexAnnotations(index);
        public static IDocumentDbKeyAnnotations DocumentDb(this IKey key)
            => new DocumentDbKeyAnnotations(key);
        public static IDocumentDbForeignKeyAnnotations DocumentDb(this IForeignKey foreignKey)
            => new DocumentDbForeignKeyAnnotations(foreignKey);
    }
}