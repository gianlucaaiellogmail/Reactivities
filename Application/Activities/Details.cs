using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Details
    {
        public class Query : IRequest<Activity>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Activity>
        {
            private readonly DataContext _dbContext;
            public Handler(DataContext dbContext)
            {
                _dbContext = dbContext;
            }

            async Task<Activity> IRequestHandler<Query, Activity>.Handle(Query request, CancellationToken cancellationToken)
            {
                var activity = await _dbContext.Activities.FindAsync(request.Id);
                return activity;
            }
        }
    }
}