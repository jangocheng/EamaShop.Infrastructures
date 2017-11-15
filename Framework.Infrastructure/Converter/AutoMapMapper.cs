using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace Framework.Infrastructure.Converter
{
    /// <inheritdoc />
    /// <summary>
    /// 基于automapper实现的<see cref="T:Framework.Infrastructure.Converter.IMapper" />
    /// </summary>
    public class AutoMapMapper : IMapper
    {
        private static bool _initialized;
        private static readonly object SyncRoot = new object();

        /// <inheritdoc />
        public AutoMapMapper(IEnumerable<IMapperStartup> startups)
        {
            if (_initialized) return;
            lock (SyncRoot)
            {
                if (_initialized) return;
                _initialized = true;
                Mapper.Initialize(cfg =>
                {
                    foreach (var startup in startups)
                    {
                        startup.Initialize(cfg);
                    }
                });
            }
        }


        /// <inheritdoc />
        public object To(object source, Type sourceType, Type desctinationType)
        {
            if (source == null) return null;
            Checker.NotNull(sourceType, nameof(sourceType));
            Checker.NotNull(desctinationType, nameof(desctinationType));
            return Mapper.Map(source, sourceType, desctinationType);
        }
    }
}
