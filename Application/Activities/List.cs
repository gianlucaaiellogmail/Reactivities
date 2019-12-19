using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
    public class List
    {
        public class Query : IRequest<List<Activity>> { }

        public class Handler : IRequestHandler<Query, List<Activity>>
        {
            private readonly DataContext _dbContext;
            public Handler(DataContext dbContext)
            {
                _dbContext = dbContext;
            }

            async Task<List<Activity>> IRequestHandler<Query, List<Activity>>.Handle(Query request, CancellationToken cancellationToken)
            {
                var activities = await _dbContext.Activities.ToListAsync();
                return activities;
            }
        }
    }
}