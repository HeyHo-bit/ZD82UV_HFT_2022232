using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ZD82UV_HFT_2022232.Endpoint.Services;
using ZD82UV_HFT_2022232.Logic;
using ZD82UV_HFT_2022232.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ZD82UV_HFT_2022232.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BandController : ControllerBase
    {
        IBandLogic logic;
        IHubContext<SignalRHub> hub;

        public BandController(IBandLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Band> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Band Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Band value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("BandCreated", value);
        }

        [HttpPut]
        public void Uptade([FromBody] Band value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("BandUptaded", value);
        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var bandToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("BandDeleted", bandToDelete);
        }
    }
}
