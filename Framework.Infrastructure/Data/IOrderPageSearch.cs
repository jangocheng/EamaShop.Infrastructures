namespace Framework.Infrastructure.Data
{
    /// <inheritdoc />
    /// <summary>
    /// 定义可排序分页搜索的必须条件
    /// </summary>
    public interface IOrderPageSearch : IPageSearch
    {
        /// <summary>
        /// 是否倒叙
        /// </summary>
        bool DescendingOrder { get; set; } 
        /// <summary>
        /// 排序的字段
        /// </summary>
        string[] OrderByFields { get; set; } 
    }
}
