using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Category { get; set; }
            public DateTime Date { get; set; }
            public string City { get; set; }
            public string Venue { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _dbContext;
            public Handler(DataContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _dbContext.Activities.FindAsync(request.Id);

                if (activity == null)
                {
                    throw new Exception("Activity not found");
                }

                activity.Title = request.Title;
                activity.Description = request.Description;
                activity.Date = request.Date;
                activity.City = request.City;
                activity.Category = request.Category;
                activity.Venue = request.Venue;

                var success = await _dbContext.SaveChangesAsync() > 0;

                if (success) return Unit.Value;
                throw new Exception("Problem saving activity");
            }
        }
    }
}