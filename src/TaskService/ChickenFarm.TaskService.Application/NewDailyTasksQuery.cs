using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ChickenFarm.Shared;
using ChickenFarm.TaskService.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ChickenFarm.TaskService.Application
{
    public class NewDailyTasksQuery
    {
        public record Query(int FarmId) : IRequest<IEnumerable<ShedDto>>;

        public class Handler : IRequestHandler<Query, IEnumerable<ShedDto>>
        {
            private readonly IRepository<Farm> _farmRepository;
            private readonly ILogger<Handler> _logger;

            public Handler(IRepository<Farm> farmRepository, ILogger<Handler> logger)
            {
                _farmRepository = farmRepository;
                _logger = logger;
            }

            public async System.Threading.Tasks.Task<IEnumerable<ShedDto>> Handle(Query request,
                CancellationToken cancellationToken)
            {
                var openTasks = await _farmRepository
                    .Get(x => x.Id == request.FarmId &&
                              x.Sheds.Any(y => y.Tasks.Any(z => z.DateTime != DateTime.Now.Date)))
                    .ConfigureAwait(false);

                var result =  openTasks.SelectMany(x => x.Sheds).ToList().Distinct();

                return result.Select(shed => new ShedDto(shed.Id));
            }
        }
    }
}