using System.Net;
using System.Text;
using Ocelot.Middleware;
using Ocelot.Multiplexer;

namespace ApiGatewayDogMate;

public class FakeDefinedAggregator : IDefinedAggregator
{
    public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
    {
        var content = new List<string>();

        foreach (var response in responses)
        {
            var downstreamContent = await response.Items.DownstreamResponse().Content.ReadAsStringAsync();
            content.Add(downstreamContent);
        }

        var aggregatedContent = $"[{string.Join(",", content)}]";
        var stringContent = new StringContent(aggregatedContent, Encoding.UTF8, "application/json");

        return new DownstreamResponse(stringContent, HttpStatusCode.OK,
            new List<KeyValuePair<string, IEnumerable<string>>>(), "application/json");
    }
}