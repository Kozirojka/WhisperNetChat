using WhisperNet.API.Interfaces;

namespace WhisperNet.API.Extensions;

public static class EndpointRegistrationExtensions
{
    
    // https://bit.ly/4h1
    // Link on topic on LinkedIn with Automatic register endpoints
    public static void RegisterAllEndpoints(this IEndpointRouteBuilder app)
    {
        var endpointDefinitions = AppDomain
            .CurrentDomain
            .GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => typeof(IEndpoint).IsAssignableFrom(type)
                && !type.IsInterface && !type.IsInterface)
            .Select(Activator.CreateInstance)
            .Cast<IEndpoint>();
    

        foreach (var endpoint in endpointDefinitions)
        {
            endpoint.RegisterEndpoints(app);
        }
    }
}