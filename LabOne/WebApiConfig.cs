using System.Web.Http;
using Swashbuckle.Application;
using System.IO;
using System.Web.Hosting;

namespace LabOne
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Enable Swagger
            config.EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "LabOne API");
                var xmlPath = System.IO.Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data"), "LabOne.xml");
                c.IncludeXmlComments(xmlPath);
            })
            .EnableSwaggerUi();
        }
    }
}
