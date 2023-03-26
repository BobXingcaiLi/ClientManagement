using ClientManagement.Application.Features.Client.Commands.CreateClient;
using ClientManagement.Application.Features.Client.Commands.DeleteClient;
using ClientManagement.Application.Features.Client.Commands.UpdateClient;
using ClientManagement.Application.Features.Client.Queries.GetAllClients;
using ClientManagement.Application.Features.Client.Queries.GetClientDetail;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClientManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ClientController : ControllerBase
    {
        
        private readonly ILogger<ClientController> _logger;
        private readonly IMediator _mediator;

        public ClientController(ILogger<ClientController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        // GET: api/<ClientController>
        [HttpGet]
        public async Task<List<ClientDto>> Get()
        {
            var clients = await _mediator.Send(new GetClientsQuery());
            return clients;
        }

        // GET api/<ClientController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientDetailDto>> Get(int id)
        {
            var client = await _mediator.Send(new GetClientDetailQuery(id));
            return Ok(client);
        }

        // POST api/<ClientController>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Post(CreateClientCommand client)
        {
            var response = await _mediator.Send(client);
            return CreatedAtAction(nameof(Get), new { id = response });
        }

        // PUT api/<ClientController>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Put(UpdateClientCommand client)
        {
            await _mediator.Send(client);
            return NoContent();
        }

        // DELETE api/<ClientController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteClientCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}