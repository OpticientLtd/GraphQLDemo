
using System.Collections.Generic;

namespace GraphQLDemo.API.GraphQL.Common
{
    public abstract class Payload<TKey, TData>
        where TKey : unmanaged
        where TData : class
    {
        public TKey key { get; }
        public TData data { get; }

        public bool success { get; } = false;
        public Payload(TKey key, TData data)
        {
            this.key = key;
            this.data = data;

            success = true;
        }
        public Payload(IReadOnlyList<UserError> errors = null)
        {
            Errors = errors;
        }
        public Payload(UserError error)
        {
            Errors = new[] { error };
        }
        public IReadOnlyList<UserError> Errors { get; }
    }
}
