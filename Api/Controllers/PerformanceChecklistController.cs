using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Repository;

namespace Api.Controllers
{
    [Route("api/performance-checklist")]
    [ApiController]
    public class PerformanceChecklistController : ControllerBase
    {
        private TeamCheckAnswerRepository _repository;
        public PerformanceChecklistController(IConfiguration configuration)
        {
            _repository = new TeamCheckAnswerRepository(configuration);
        }

        [HttpGet]
        public IEnumerable<TeamCheckAnswer> ListAnswers()
        {
            var answers = _repository.GetAll();
            return answers;
        }

        [HttpPost]
        public async Task NewSubmission(TeamCheckItem[] checklist)
        {
            var answer = new TeamCheckAnswer
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                items = checklist
            };
            await _repository.SaveAnswer(answer);
            foreach(var item in checklist)
            {
                Console.WriteLine(item.Key + ": " + item.Value);
            }
        }
    }
}

