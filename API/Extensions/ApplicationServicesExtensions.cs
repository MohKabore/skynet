using System.Linq;
using API.Controllers.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.Configure<ApiBehaviorOptions>(options =>
                 {
                     options.InvalidModelStateResponseFactory = ActionContext =>
                     {
                         var erros = ActionContext.ModelState
                                     .Where(erros => erros.Value.Errors.Count > 0)
                                     .SelectMany(x => x.Value.Errors)
                                     .Select(x => x.ErrorMessage).ToArray();

                         var errorResponse = new ApiValidationErrorResponse
                         {
                             Erros = erros
                         };
                         return new BadRequestObjectResult(errorResponse);
                     };
                 });
            return services;
        }

    }
}