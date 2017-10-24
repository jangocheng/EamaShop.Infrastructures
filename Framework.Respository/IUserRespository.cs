using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Framework.Domain;
using Framework.Infrastructure.DataPages;

namespace Framework.Respository
{
    public interface IUserRespository : IRespository<User>
    {
        User FindByPhone(string phone);
        IEnumerable<User> SearchDesc(Expression<Func<User, bool>> condition, IPageSearch page, params Expression<Func<User, object>>[] orders);

        long Count(Expression<Func<User, bool>> condition);
    }
}
