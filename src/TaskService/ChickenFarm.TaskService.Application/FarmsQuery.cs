using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ChickenFarm.Shared;
using ChickenFarm.TaskService.Domain;
using MediatR;

namespace ChickenFarm.TaskService.Application
{
    public class FarmsQuery
    {
        public record Query(int FarmId) : IRequest<IEnumerable<FarmDto>>;

        public class Handler : IRequestHandler<Query, IEnumerable<FarmDto>>
        {
            private readonly IRepository<Farm> _farmRepository;

            public Handler(IRepository<Farm> farmRepository) =>
                _farmRepository = farmRepository;

            public async Task<IEnumerable<FarmDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var farmData = await _farmRepository.Get(x => x.Id == request.FarmId).ConfigureAwait(false);
                return farmData.Select(farm => new FarmDto(farm.Id, farm.Sheds.Select(y => new ShedDto(y.Id)).ToList()));
            }
        }
    }
}