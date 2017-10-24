namespace Framework.Infrastructure.Converter
{
    /// <summary>
    /// mapper的常用扩展
    /// </summary>
    public static class MapperExtensions
    {
        /// <summary>
        /// 将指定的对象转换为另一种对象
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="mapper"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TDestination To<TDestination>(this IMapper mapper, object source)
        => (TDestination)mapper.To(source, source.GetType(), typeof(TDestination));
    }
}
