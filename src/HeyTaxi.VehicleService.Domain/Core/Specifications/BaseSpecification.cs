using System.Linq.Expressions;

namespace HeyTaxi.VehicleService.Domain.Core.Specifications;

public sealed class BaseSpecification<T> : ISpecification<T>
{
    public BaseSpecification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }
    
    public Expression<Func<T, bool>>? Criteria { get; }
    public List<Expression<Func<T, object>>>? Includes { get; } = new List<Expression<Func<T, object>>>();
    public List<Expression<Func<T, object>>>? Selects { get; } = new List<Expression<Func<T, object>>>();
    public List<string>? IncludeStrings { get; } = new List<string>();
    public Expression<Func<T, object>>? OrderBy { get; private set; }
    public Expression<Func<T, object>>? OrderByDescending { get; private set; }
    public Expression<Func<T, object>>? GroupBy { get; private set; }

    public int Take { get; private set; }
    public int Skip { get; private set; }
    public bool IsPagingEnabled { get; private set; }

    public void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes?.Add(includeExpression);
    }
    public void AddSelect(Expression<Func<T, object>> selectExpression)
    {
        Selects?.Add(selectExpression);
    }
    
    public void AddInclude(string includeString)
    {
        IncludeStrings?.Add(includeString);
    }
    
    public void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
    }
    
    public void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }
    
    public void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
    {
        OrderByDescending = orderByDescendingExpression;
    }
    
    public void ApplyGroupBy(Expression<Func<T, object>> groupByExpression)
    {
        GroupBy = groupByExpression;
    }
}