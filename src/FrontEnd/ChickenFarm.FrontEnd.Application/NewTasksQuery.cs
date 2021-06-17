using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ChickenFarm.FrontEnd.Domain;
using ChickenFarm.Shared;
using MediatR;

namespace ChickenFarm.FrontEnd.Application
{
    public class NewTasksQuery
    {
        public record Query(int FarmId) : IRequest<IEnumerable<ShedDto>>;

        public class Handler : IRequestHandler<Query, IEnumerable<ShedDto>>
        {
            private readonly ITasksService _tasksService;

            public Handler(ITasksService tasksService) =>
                _tasksService = tasksService;

            public async Task<IEnumerable<ShedDto>> Handle(Query request, CancellationToken cancellationToken) =>
                await _tasksService.GetTasks(request.FarmId).ConfigureAwait(false);
        }
    }
}