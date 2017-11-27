using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Framework.Tools
{
    public sealed class ExpressionTests
    {
        [Fact]
        public void ExtraParameter_Test()
        {
            //var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
            //var json = File.ReadAllText("TextFile1.txt");
            //var vv = new HttpClient().PutAsync("http://localhost:59632/Commodity", new StringContent(json, Encoding.UTF8, "application/json")).Result.Content.ReadAsStringAsync().Result;
            Expression<Func<Foo, bool>> wh = (f) => f.Age == 1;

            var a = Create((Foo s) => new { Age = s.Age });
            Expression<Func<Foo, Foo1>> map = (s) => new Foo1() { Age = s.Age };
            var source = new QuerableDbSet<Foo>();
            var result = source.Where(wh).Append(new Foo1());//.ToList();

            result = result.Cast<Foo>();
            result = result.DefaultIfEmpty();
            result = result.Distinct();
            result.ToList();
            var store = Create<Func<IQueryable<Foo>, Expression<Func<Foo, bool>>, IQueryable<Foo>>>((S, W) => Queryable.Where(S, W));
            //Concat
            //DefaultIfEmpty
            //Distinct
            //Except
            //GroupBy
            //GroupJoin
            //Intersect
            //Join
            //OfType
            //OrderBy
            //Prepend
            //Reverse
            //Select
            //SelectMany
            //Skip
            //SkipLast
            //SkipWhile
            //Take
            //TakeLast
            //TakeWhile
            //ThenBy
            //Union
            //Where
            //Zip

        }

        private Expression Create<FuncLo>(Expression<FuncLo> lo)
        {
            return lo.Body;
        }
        private Expression<Func<TSource, TResult>> Create<TSource, TResult>(Expression<Func<TSource, TResult>> targ)
        {
            return targ;
        }

        private class QuerableDbSet<TTable> : IQueryable<TTable>
        {
            internal QuerableDbSet() : this(new QuerableDbSetProvider())
            {

            }
            internal QuerableDbSet(IQueryProvider provider)
            {
                _provider = provider;
                _expression = Expression.Constant(this);
            }
            internal QuerableDbSet(IQueryProvider provider, Expression expression)
            {
                _provider = provider;
                _expression = expression;
            }
            Type IQueryable.ElementType => typeof(TTable);
            Expression IQueryable.Expression => _expression;
            IQueryProvider IQueryable.Provider => _provider;

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEumerator();
            }

            IEnumerator<TTable> IEnumerable<TTable>.GetEnumerator()
            {
                return GetEumerator();
            }
            private IEnumerator<TTable> GetEumerator()
            {
                return _provider.Execute<IEnumerable<TTable>>(_expression).GetEnumerator();
            }
            private IQueryProvider _provider;
            private Expression _expression;
        }

        private class QuerableDbSetProvider : IQueryProvider
        {
            IQueryable IQueryProvider.CreateQuery(Expression expression)
            {
                return (IQueryable)Activator.CreateInstance(typeof(QuerableDbSet<>).MakeGenericType(expression.Type));
            }

            IQueryable<TElement> IQueryProvider.CreateQuery<TElement>(Expression expression)
            {
                return new QuerableDbSet<TElement>(this, expression);
            }

            object IQueryProvider.Execute(Expression expression)
            {
                return null;
            }

            TResult IQueryProvider.Execute<TResult>(Expression expression)
            {
                // must is method
                if (expression is MethodCallExpression method)
                {
                    // where().OrderBy(x=>x.Member).Where(x=>x.Member>value)
                    // (select * from table where (conditions) order by member) where ()
                    // 
                    //arg1 must be source IQuerable<T> or IQuerable<T> Method()
                    // explain second

                }
                return default(TResult);
            }
        }
    }
}
