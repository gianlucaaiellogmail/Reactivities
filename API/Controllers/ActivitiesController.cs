using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Activities;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ActivitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/activites
        [HttpGet]
        public async Task<ActionResult<List<Activity>>> Get()
        {
            var activities = await _mediator.Send(new List.Query());
            return Ok(activities);
        }

        // GET api/activites/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> Get(Guid id)
        {
            var activity = await _mediator.Send(new Details.Query{Id = id});
            return Ok(activity);
        }
    
        // POST api/activities
        [HttpPost]
        public async Task<ActionResult<Unit>> Post([FromBody] Create.Command command)
        {
            return await _mediator.Send(command);
        }

        // PUT api/activities
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Put(Guid id, [FromBody] Edit.Command command)
        {
            command.Id = id;
            return await _mediator.Send(command);
        }

        // DELETE api/activities
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Delete(Guid id)
        {
            return await _mediator.Send(new Delete.Command{Id = id});
        }
    }
}