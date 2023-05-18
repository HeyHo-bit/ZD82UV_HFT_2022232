using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
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

        public BandController(IBandLogic logic)
        {
            this.logic = logic;
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
        }

        [HttpPut]
        public void Uptade([FromBody] Band value)
        {
            this.logic.Update(value);
        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}
