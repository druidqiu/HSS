using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSS.Web.Framework.Grid
{
    /// <summary>
    /// 列表过滤器
    /// </summary>
    public class Filter
    {
        /// <summary>
        /// 排序字段（属性）的名称。设置为“空”的，如果< >过滤器>属性被设置
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// 过滤运算符。设置为“空”的，如果< >过滤器>属性被设置
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 过滤值。设置为“空”的，如果< >过滤器>属性被设置
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 过滤逻辑。可以设置为“或”或“和”。设置为“空”的，除非是一组
        /// </summary>
        public string Logic { get; set; }

        /// <summary>
        /// 筛选器表达式。如果没有儿童表达式，则设置为
        /// </summary>
        public IEnumerable<Filter> Filters { get; set; }
        
        private static readonly IDictionary<string, string> operators = new Dictionary<string, string>
        {
            {"eq", "="},
            {"neq", "!="},
            {"lt", "<"},
            {"lte", "<="},
            {"gt", ">"},
            {"gte", ">="},
            {"startswith", "StartsWith"},
            {"endswith", "EndsWith"},
            {"contains", "Contains"},
            {"doesnotcontain", "DoesNotContain"}
        };
        
        public IList<Filter> All()
        {
            var filters = new List<Filter>();

            Collect(filters);

            return filters;
        }

        private void Collect(IList<Filter> filters)
        {
            if (Filters != null && Filters.Any())
            {
                foreach (Filter filter in Filters)
                {
                    filters.Add(filter);

                    filter.Collect(filters);
                }
            }
            else
            {
                filters.Add(this);
            }
        }
        
        public string ToExpression(IList<Filter> filters)
        {
            if (Filters != null && Filters.Any())
            {
                return "(" + String.Join(" " + Logic + " ", Filters.Select(filter => filter.ToExpression(filters)).ToArray()) + ")";
            }

            int index = filters.IndexOf(this);

            string comparison = operators[Operator];

            //original code below (case sensitive) commented
            //if (comparison == "StartsWith" || comparison == "EndsWith" || comparison == "Contains")
            //{
            //    return String.Format("{0}.{1}(@{2})", Field, comparison, index);
            //}

            //we ignore case
            if (comparison == "Contains")
            {
                return String.Format("{0}.IndexOf(@{1}, System.StringComparison.InvariantCultureIgnoreCase) >= 0", Field, index);
            }
            if (comparison == "DoesNotContain")
            {
                return String.Format("{0}.IndexOf(@{1}, System.StringComparison.InvariantCultureIgnoreCase) < 0", Field, index);
            }
            if (comparison == "=" && Value.GetType() == typeof(String))
            {
                //string only
                comparison = "Equals";
                //numeric values use standard "=" char
            }
            if (comparison == "StartsWith" || comparison == "EndsWith" || comparison == "Equals")
            {
                return String.Format("{0}.{1}(@{2}, System.StringComparison.InvariantCultureIgnoreCase)", Field, comparison, index);
            }

            return String.Format("{0} {1} @{2}", Field, comparison, index);
        }
    }
}
