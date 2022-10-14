using EmployeeManager.Domain.Core.Exceptions;
using EmployeeManager.Services.Interfaces.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmployeeManager.Handlers
{
    internal class ExceptionHandler
    {
        private readonly RequestDelegate _next;

        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "text/plain";

                var statusCode = StatusCodes.Status500InternalServerError;              

                switch (error)
                {
                    case ConflictException:
                        statusCode = StatusCodes.Status409Conflict;
                        break;
                    case NotFoundException:
                        statusCode = StatusCodes.Status404NotFound;
                        break;
                    case BadRequestException:
                        statusCode = StatusCodes.Status400BadRequest;
                        break;
                    case UnprocessableEntityException:
                        statusCode = StatusCodes.Status422UnprocessableEntity;
                        break;
                }

                response.StatusCode = statusCode;
                var content = Encoding.UTF8.GetBytes($"Error [{error.Message}]");
                await response.Body.WriteAsync(content, 0, content.Length);
            }
        }

    }
}
