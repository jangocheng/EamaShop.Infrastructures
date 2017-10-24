namespace Framework.Domain.MessageModels
{
    /// <summary>
    /// 发布文章，推送给所有微信会员的消息实体
    /// </summary>
    public class PushArticleMessage
    {

        /// <summary>
        /// 初始化 <see cref="PushArticleMessage"/> 类型的新实例
        /// </summary>
        public PushArticleMessage() { }
        /// <summary>
        /// 初始化 <see cref="PushArticleMessage"/> 类型的新实例
        /// </summary>
        public PushArticleMessage(long articleId)
        {
            ArticleId = articleId;
        }
        /// <summary>
        /// 文章的Id
        /// </summary>
        public long ArticleId { get; set; }
    }
}
