using System;
using System.Collections.Generic;

namespace FastTypes.Query
{
    public sealed class QueryTags
    {
        public static readonly QueryTags Empty = new(new Dictionary<Type, object>(0));

        public static QueryTags FromDictionary(Dictionary<Type, object> tags)
        {
            if (tags == null) throw new ArgumentNullException(nameof(tags));
            if (tags.Count == 0) return Empty;

            return new QueryTags(tags);
        }

        private readonly Dictionary<Type, object> _tags;

        private QueryTags(Dictionary<Type, object> tags)
        {
            _tags = tags;
        }

        public bool TryGet<TTag>(out TTag result)
        {
            if (_tags.TryGetValue(typeof(TTag), out var r))
            {
                result = (TTag)r;
                return true;
            }

            result = default;
            return false;
        }
    }
}