using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace GraphQL.Client.Http
{
    /// <summary>
    /// The Options that the <see cref="GraphQLHttpClient"/> will use
    /// </summary>
    public class GraphQLHttpClientOptions
    {
        /// <summary>
        /// The GraphQL EndPoint to be used
        /// </summary>
        public Uri EndPoint { get; set; }

        /// <summary>
        /// The <see cref="System.Net.Http.HttpMessageHandler"/> that is going to be used
        /// </summary>
        public HttpMessageHandler HttpMessageHandler { get; set; } = new HttpClientHandler();

        /// <summary>
        /// The <see cref="MediaTypeHeaderValue"/> that will be send on POST
        /// </summary>
        public string MediaType { get; set; } = "application/json"; // This should be "application/graphql" also "application/x-www-form-urlencoded" is Accepted

        /// <summary>
        /// The back-off strategy for automatic websocket/subscription reconnects. Calculates the delay before the next connection attempt is made.<br/>
        /// default formula: min(n, 5) * 1,5 * random(0.0, 1.0)
        /// </summary>
        public Func<int, TimeSpan> BackOffStrategy { get; set; } = n =>
        {
            var rnd = new Random();
            return TimeSpan.FromSeconds(Math.Min(n, 5) * 1.5 + rnd.NextDouble());
        };

        /// <summary>
        /// If <see langword="true"/>, the websocket connection is also used for regular queries and mutations
        /// </summary>
        public bool UseWebSocketForQueriesAndMutations { get; set; } = false;

        /// <summary>
        /// Request preprocessing function. Can be used i.e. to inject authorization info into a GraphQL request payload.
        /// </summary>
        public Func<GraphQLRequest, GraphQLHttpClient, Task<GraphQLHttpRequest>> PreprocessRequest { get; set; } = (request, client) =>
            Task.FromResult(request is GraphQLHttpRequest graphQLHttpRequest ? graphQLHttpRequest : new GraphQLHttpRequest(request));

        /// <summary>
        /// This callback is called after successfully establishing a websocket connection but before any regular request is made. 
        /// </summary>
        public Func<GraphQLHttpClient, Task> OnWebsocketConnected { get; set; } = client => Task.CompletedTask;

        /// <summary>
        /// Configure additional websocket options (i.e. headers). This will not be invoked on Windows 7 when targeting .NET Framework 4.x. 
        /// </summary>
        public Action<ClientWebSocketOptions> ConfigureWebsocketOptions { get; set; } = options => { };
    }
}
