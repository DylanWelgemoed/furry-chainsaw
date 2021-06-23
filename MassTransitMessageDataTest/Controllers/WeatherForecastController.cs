using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.MessageData;
using MassTransitMessageDataTest.Commands;
using Microsoft.AspNetCore.Http;

namespace MassTransitMessageDataTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IMessageDataRepository _dataRepository;
        private readonly IBus _bus;

        public WeatherForecastController(IMessageDataRepository dataRepository, IBus bus)
        {
            _dataRepository = dataRepository;
            _bus = bus;
        }

        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> BigMessage(string id, IFormFile file, CancellationToken cancellationToken)
        {
            BigMessage command;
            await using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms, cancellationToken);

                var messageData = await this._dataRepository.PutStream(ms, cancellationToken);
                command = new BigMessage(id, messageData);
            }

            await this._bus.Publish(command, cancellationToken);

            return this.StatusCode(StatusCodes.Status200OK, "Hi");
        }
    }
}
