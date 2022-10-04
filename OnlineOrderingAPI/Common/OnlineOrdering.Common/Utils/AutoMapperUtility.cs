using AutoMapper;

namespace OnlineOrdering.Common.Utils
{
    /// <summary>
    /// Gets the mapped object.
    /// </summary>
    /// <typeparam name="S">Source type</typeparam>
    /// <typeparam name="T">Destination Type</typeparam>
    public static class AutoMapperUtility<S, T>
    {
        /// <summary>
        /// Gets the mapped object.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="mapper">The mapper.</param>
        /// <returns></returns>
        public static T GetMappedObject(S source, IMapper mapper)
        {
            return mapper.Map<T>(source);
        }
    }
}
