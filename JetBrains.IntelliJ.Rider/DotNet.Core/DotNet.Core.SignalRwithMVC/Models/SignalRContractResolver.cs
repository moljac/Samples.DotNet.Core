using System.Reflection;

namespace DotNet.Core.SignalR.Models
{
    /*
simply cannot force all components to pass camel case objects because it would break current conventions in place.

Simply put, if you try this the JavaScript client will no longer connect, because all connection and internal communication is tansformed to camel case.

We are at the point where we need all of the application objects to be passed camel cased, and all connection and SignalR internal objects to be unmodified.

We will write a custom contract resolver that looks at the assembly of the object type and if it is not an internal SignalR object (if it is not from the same assembly as Connection, a class from SignalR), then it modifies it to be camel case:
    */
    public class SignalRContractResolver  : Newtonsoft.Json.Serialization.IContractResolver
    {
        private readonly System.Reflection.Assembly _assembly;
        private readonly Newtonsoft.Json.Serialization.IContractResolver _camelCaseContractResolver;
        private readonly Newtonsoft.Json.Serialization.IContractResolver _defaultContractSerializer;

        public SignalRContractResolver()
        {
            _defaultContractSerializer =
                new Newtonsoft.Json.Serialization.DefaultContractResolver();
            _camelCaseContractResolver =
                new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            _assembly =
                typeof(Microsoft.AspNetCore.SignalR.Infrastructure.Connection)
                    .GetTypeInfo()
                    .Assembly
                    ;
        }


        public Newtonsoft.Json.Serialization.JsonContract ResolveContract(System.Type type)
        {
            if (type.GetTypeInfo().Assembly.Equals(_assembly))
            {
                return _defaultContractSerializer.ResolveContract(type);
            }

            return _camelCaseContractResolver.ResolveContract(type);
        }

    }

}