using System;
using System.Collections.Generic;

namespace FastTypes.Query
{
    /// <summary>
    /// Represents tags that can be stored for a query.
    /// </summary>
    public sealed class QueryTags
    {
        /// <summary>
        /// Represents an empty instance of QueryTags.
        /// </summary>
        public static readonly QueryTags Empty = new(new Dictionary<Type, object>(0));

        /// <summary>
        /// Creates a new instance of QueryTags from a dictionary of tags.
        /// </summary>
        /// <param name="tags">The dictionary of tags.</param>
        /// <returns>A new instance of QueryTags.</returns>
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

        /// <summary>
        /// Tries to get a tag of type TTag from the QueryTags instance.
        /// </summary>
        /// <typeparam name="TTag">The type of the tag.</typeparam>
        /// <param name="result">The result value of the tag.</param>
        /// <returns>True if the tag is found; otherwise, false.</returns>
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