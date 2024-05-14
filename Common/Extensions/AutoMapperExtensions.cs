using AutoMapper;

namespace minimal_api_template.Common.Extensions;

public static class AutoMapperExtensions
{
    public static async Task<TResult?> MapTo<TSource, TResult>(
        this Task<TSource> task,
        IMapper mapper
    )
    {
        var item = await task.ConfigureAwait(false);

        return mapper.Map<TResult>(item);
    }
}