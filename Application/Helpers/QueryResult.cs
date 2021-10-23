using System.Collections.Generic;

namespace Application.Helpers
{
    public class QueryResult<T> where T : class
    {
        private readonly HashSet<T> _queryResultList;

        private QueryResult()
        {
            _queryResultList = new();
        }

        internal QueryResult(IEnumerable<T>? response, T? data = null) : this()
        {
            if (response is not null)
                foreach (var item in response)
                    _queryResultList.Add(item);

            if (data is not null)
                Result = data;
        }

        public IReadOnlyCollection<T> Results => _queryResultList;
        public T? Result { get; private set; }
        
        public static QueryResult<T> CreateQueryResults(IEnumerable<T> response) => new(response);

        public static QueryResult<T> CreateQueryResult(T data) => new(null, data);
    }
}
