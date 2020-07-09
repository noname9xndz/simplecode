using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using RedisConfig.Base;
using System.Text;
using RedisConfig.Service;

namespace RedisExample.Controllers
{
    using RedisExample.Models;

    public class HomeController : Controller
    {
        private readonly RedisConfiguration _redis;
        private readonly IDistributedCache _cache;
        private readonly IRedisConnectionFactory _fact;
        private readonly IRedisGenericService<Vote> _redisVoteService;

        public HomeController(IOptions<RedisConfiguration> redis, IDistributedCache cache
            , IRedisConnectionFactory factory, IRedisGenericService<Vote> redisVoteService)
        {
            _redis = redis.Value;
            _cache = cache;
            _fact = factory;
            _redisVoteService = redisVoteService;
        }

        public IActionResult Index()
        {
            var helloRedis = Encoding.UTF8.GetBytes("Hello Redis");
            HttpContext.Session.Set("hellokey", helloRedis);

            var getHello = default(byte[]);
            HttpContext.Session.TryGetValue("hellokey", out getHello);
            ViewData["Hello"] = Encoding.UTF8.GetString(getHello);
            ViewData["SessionID"] = HttpContext.Session.Id;

            _cache.SetString("CacheTest", "Redis is awesome");

            ViewData["Host"] = _redis.Host;
            ViewData["Port"] = _redis.Port;
            ViewData["Name"] = _redis.Name;

            ViewData["DistCache"] = _cache.GetString("CacheTest");

            var db = _fact.Connection().GetDatabase();
            db.StringSet("StackExchange.Redis.Key", "Stack Exchange Redis is Awesome");
            ViewData["StackExchange"] = db.StringGet("StackExchange.Redis.Key");

            return View();
        }

        public PartialViewResult Vote(string value)
        {
            var theVote = new Vote();
            switch (value)
            {
                case "Y":
                    theVote.Yes = 1;
                    break;

                case "N":
                    theVote.No = 1;
                    break;

                case "U":
                    theVote.Undecided = 1;
                    break;

                case "R":
                    _redisVoteService.Delete("RedisVote");
                    break;

                default: break;
            }
            _redisVoteService.Save("RedisVote", theVote);

            var model = _redisVoteService.Get("RedisVote");
            return this.PartialView("~/Views/Home/Vote.cshtml", model);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}