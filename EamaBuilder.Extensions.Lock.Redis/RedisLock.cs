using EamaBuilder.Extensions.Lock.Abstractions;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EamaBuilder.Extensions.Lock.Redis
{
    /// <summary>
    /// This class support for EamaBuilder Infrastructures.
    /// Do not use it in your application code directly.
    /// </summary>
    public class RedisLock : IDistributedLock, IEquatable<RedisLock>
    {
        private readonly IDatabase _database;
        private string TOKEN = "redis_locked_token";
        /// <summary>
        /// init
        /// </summary>
        /// <param name="database"></param>
        /// <param name="name"></param>
        public RedisLock(IDatabase database, string name)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
        /// <summary>
        /// Unique name of this lock.
        /// </summary>
        public string Name { get; }
        ///<inheritdoc />
        public bool Enter(TimeSpan expired)
        {
            return _database.LockTake(Name, TOKEN, expired);
        }

        ///<inheritdoc />
        public Task<bool> EnterAsync(TimeSpan expired, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            return _database.LockTakeAsync(Name, TOKEN, expired);
        }

        ///<inheritdoc />
        public override bool Equals(object obj)
        {
            return Equals(obj as RedisLock);
        }

        ///<inheritdoc />
        public bool Equals(RedisLock other)
        {
            return other != null &&
                   TOKEN == other.TOKEN &&
                   Name == other.Name;
        }

        ///<inheritdoc />
        public void Exit()
        {
            _database.LockRelease(Name, TOKEN);
        }

        ///<inheritdoc />
        public Task ExitAsync()
        {
            return _database.LockReleaseAsync(Name, TOKEN);
        }

        ///<inheritdoc />
        public override int GetHashCode()
        {
            var hashCode = -1949558680;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TOKEN);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            return hashCode;
        }

        ///<inheritdoc />
        public static bool operator ==(RedisLock lock1, RedisLock lock2)
        {
            return EqualityComparer<RedisLock>.Default.Equals(lock1, lock2);
        }

        ///<inheritdoc />
        public static bool operator !=(RedisLock lock1, RedisLock lock2)
        {
            return !(lock1 == lock2);
        }
    }
}
