namespace minimal_api_template.Common;

public interface IEndpoint
{
    static abstract void Map(IEndpointRouteBuilder app);
}