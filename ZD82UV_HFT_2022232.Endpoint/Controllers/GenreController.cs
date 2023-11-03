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
    public class GenreController : ControllerBase
    {
        IGenreLogic logic;
        IHubContext<SignalRHub> hub;

        public GenreController(IGenreLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Genre> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Genre Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Genre value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("GenreCreated", value);
        }

        [HttpPut]
        public void Uptade([FromBody] Genre value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("GenreUpdated", value);
        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var genreToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("GenreDeleted", genreToDelete);
        }
    }
}
