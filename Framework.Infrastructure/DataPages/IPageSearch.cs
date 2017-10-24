namespace Framework.Infrastructure.DataPages
{
    /// <summary>
    /// 定义分页搜索的必须条件
    /// </summary>
    public interface IPageSearch
    {
        /// <summary>
        /// 页码
        /// </summary>
        int PageIndex { get; set; }
        /// <summary>
        /// 每页的数据数量
        /// </summary>
        int PageSize { get; set; }
    }
}
