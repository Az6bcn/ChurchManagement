using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebApi.Helpers;

namespace WebApi.Middlewares
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // call next middleware in the pipeline
            try
            {
                await _next(context);
            }
            catch (Exception e) // handle error exceptions when try to call next middleware
            {
                await HandleExceptionAsync(e, context);
            }
        }

        private async Task HandleExceptionAsync(Exception e,
                                                HttpContext context)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = StatusCodes.Status400BadRequest;

            var errorList = new List<string>();
            
            errorList.Add(e.Message);

            foreach (DictionaryEntry d in e.Data )
                errorList.Add($"{d.Value}");

            var apiResponse = ApiRequestResponse<string>.Fail(errorList);

            var result = JsonSerializer.Serialize(apiResponse);

            await response.WriteAsync(result);
        }
    }
}