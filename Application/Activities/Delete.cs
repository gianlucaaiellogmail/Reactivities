using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
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

                _dbContext.Remove(activity);
                
                var success = await _dbContext.SaveChangesAsync() > 0;

                if (success) return Unit.Value;
                throw new Exception("Problem saving activity");
            }
        }
    }
}