using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Response.Infrastructure.Persistence.Extensions;

public static class EntityTypeBuilderExtensions
{
    public static LambdaExpression BuildLambdaForTenantFilter(Type entityType, Guid tenantId)
    {
        var parameter = Expression.Parameter(entityType, "e");
        var property = Expression.Property(parameter, "TenantId");
        var value = Expression.Constant(tenantId);
        var equal = Expression.Equal(property, value);
        return Expression.Lambda(equal, parameter);
    }
}