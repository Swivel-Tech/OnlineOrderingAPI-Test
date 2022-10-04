using AutoMapper;

namespace OnlineOrdering.Services
{
    public class BaseService
    {
        /// <summary>
        /// The mapper
        /// </summary>
        protected readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <exception cref="System.ArgumentNullException">mapper</exception>
        public BaseService(IMapper mapper)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
    }
}
