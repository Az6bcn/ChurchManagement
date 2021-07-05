using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.Helpers
{
    public class ApiRequestResponse<T> where T: class
    {
        public T Result { get; private set; }
        public bool Success { get; private set; }
        public ICollection<T> Results { get; private set; }
        public ICollection<string> ErrorMessage { get; private set; }

        public ApiRequestResponse()
        {
            
        }

        /// <summary>
        /// Parse to my custom Response class
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static ApiRequestResponse<T> Succeed(T result) => new ApiRequestResponse<T>
        {
            Success = true, 
            Result = result
        };
        
        /// <summary>
        /// Parse to my custom Response class
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        public static ApiRequestResponse<T> Succeed(ICollection<T> results) => new ApiRequestResponse<T>
        {
            Success = true,
            Results = results
        };
        
        /// <summary>
        /// Parse to my custom Response class
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static ApiRequestResponse<T> Fail(params string[] errorMessage) => new ApiRequestResponse<T> { ErrorMessage = errorMessage };

        /// <summary>
        /// Parse to my custom Response class
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static ApiRequestResponse<T> Fail(ICollection<string> errorMessage) => new ApiRequestResponse<T> { ErrorMessage
         = errorMessage };

        
    }
}