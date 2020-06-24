using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericMongoRepository.Mongo.Entities;
using GenericMongoRepository.Mongo.Infrastructure;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GenericMongoRepository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MongoController : ControllerBase
    {
        private readonly IMongoRepository<Person> _peopleRepository;

        public MongoController(IMongoRepository<Person> peopleRepository)
        {
            _peopleRepository = peopleRepository;
        }

        [HttpPost("registerPerson")]
        public async Task AddPerson(string firstName, string lastName)
        {
            var person = new Person()
            {
                FirstName = "John",
                LastName = "Doe"
            };

            await _peopleRepository.InsertOneAsync(person);
        }

        [HttpGet("getPeopleData")]
        public IEnumerable<Person> GetPeopleData()
        {
            //var people = _peopleRepository.FilterBy(x => x.FirstName != "test",projection => projection.FirstName);
            var people = _peopleRepository.FilterBy(x => x.FirstName != "test");
            return people;
        }
    }
}
