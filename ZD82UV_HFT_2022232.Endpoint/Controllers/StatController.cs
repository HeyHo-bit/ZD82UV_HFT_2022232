using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using ZD82UV_HFT_2022232.Endpoint.Services;
using ZD82UV_HFT_2022232.Logic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ZD82UV_HFT_2022232.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        ISongLogic logic;
        IGenreLogic genreLogic;
        IHubContext<SignalRHub> hub;

        public StatController(ISongLogic logic, IGenreLogic genreLogic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.genreLogic = genreLogic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<YearInfo> YearStatistics(int year)
        {
            return this.logic.YearStatistics();
        }

        [HttpGet]
        public IEnumerable<BestSo> BestSong()
        {
            return this.logic.BestSong();
            this.hub.Clients.All.SendAsync("BestSong");
        }

        [HttpGet]
        public IEnumerable<Topla> TopLabel()
        {
            return this.logic.TopLabel();
        }

        [HttpGet]
        public IEnumerable<LabelReve> LabelRevenu()
        {
            return this.logic.LabelRevenu();
        }

        [HttpGet]
        public IEnumerable<MostSo> MostSong()
        {
            return this.genreLogic.MostSong();
        }

    }
}
