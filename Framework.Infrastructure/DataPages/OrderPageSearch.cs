

using Framework.Domain.Abstractions;

namespace Framework.Infrastructure.DataPages
{
    /// <inheritdoc cref="IOrderPageSearch" />
    public class OrderPageSearch : PageSearch, IOrderPageSearch
    {

        /// <inheritdoc />
        /// <summary>
        /// 是否使用倒序的排序 默认为true
        /// </summary>
        public virtual bool DescendingOrder { get; set; } = true;
        /// <inheritdoc />
        /// <summary>
        /// 排序的字段集合
        /// </summary>
        public string[] OrderByFields { get; set; } = { nameof(IStatusRecord.ModifiedTime) };
    }
}
