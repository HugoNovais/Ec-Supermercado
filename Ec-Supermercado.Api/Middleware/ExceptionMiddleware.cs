﻿using Ec_Supermercado.Api.Erros;
using System.Net;
using System.Text.Json;

namespace Ec_Supermercado.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _netx;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate netx, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _netx = netx;
            _logger = logger;
            _env = env;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _netx(context);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = _env.IsDevelopment() ?
                    new ApiException(context.Response.StatusCode.ToString(), ex.Message, ex.StackTrace.ToString()) :
                    new ApiException(context.Response.StatusCode.ToString(), ex.Message, "Internal Server error");

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, options);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
