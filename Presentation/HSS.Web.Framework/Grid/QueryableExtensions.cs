using System.Collections.Generic;
using System.Linq;

namespace HSS.Web.Framework.Grid
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Filter<T>(this IQueryable<T> queryable, Filter filter)
        {
            if (filter != null && filter.Logic != null)
            {
                // 收集所有过滤器的列表
                var filters = filter.All();

                // 过滤器值为数组（由动态LINQ方法需要的地方）
                var values = filters.Select(f => f.Value).ToArray();

                // 创建表达式
                string predicate = filter.ToExpression(filters);

                // 动态LINQ过滤数据的方法
                queryable = queryable.Where(predicate, values);
            }

            return queryable;
        }

        public static IQueryable<T> Sort<T>(this IQueryable<T> queryable, IEnumerable<Sort> sort)
        {
            if (sort != null && sort.Any())
            {
                // 创建有序表达式
                var ordering = string.Join(",", sort.Select(s => s.ToExpression()));

                // LINQ OrderBy方法对数据进行排序
                return queryable.OrderBy(ordering);
            }

            return queryable;
        }
    }
}
