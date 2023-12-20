using System;
using System.Reflection;

namespace Vues.Net;

public static class WebApplicationExtensions
{
    public static void RegisterEndpoints(this WebApplication app, IApiPaths apiPaths)
    {
        foreach (Type mytype in Assembly.GetExecutingAssembly().GetTypes()
             .Where(mytype => mytype.GetInterfaces().Contains(typeof(IEndpoint))))
        {
            var endpoint = (IEndpoint?)Activator.CreateInstance(mytype);

            endpoint?.DefineEndpoint(app);
        }


        var paths = apiPaths.GetType()
                            .GetFields(BindingFlags.Public | BindingFlags.Static)
                            .Where(f => f.FieldType == typeof(string)
                                    && f.IsLiteral
                                    && !f.IsInitOnly)
                            .Select(f => (string)f.GetValue(null)!)
                            .ToDictionary(f => f, app.MapGroup);


        foreach (Type mytype in Assembly.GetExecutingAssembly().GetTypes()
             .Where(mytype => mytype.GetInterfaces().Contains(typeof(IGroupedEndpoint))))
        {
            var endpoint = (IGroupedEndpoint?)Activator.CreateInstance(mytype);

            endpoint?.DefineEndpoint(paths[endpoint.ApiGroup]);
        }
    }
}