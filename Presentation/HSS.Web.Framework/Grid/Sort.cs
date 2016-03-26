namespace HSS.Web.Framework.Grid
{
    /// <summary>
    /// 排序方法
    /// </summary>
    public class Sort
    {
        /// <summary>
        /// 排序字段名
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// 排序方向，升序或者降序
        /// </summary>
        public string Dir { get; set; }

        /// <summary>
        /// 形成动态的表达式 例如 ： FieldName desc
        /// </summary>
        public string ToExpression()
        {
            return Field + " " + Dir;
        }
    }
}
