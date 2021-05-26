using API.Models;
using API.Models.Dtos;
using API.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeadlocksController : Controller
    {
        private readonly IDeadlockRepository _dRepository;
        private readonly IMapper _mapper;
        public DeadlocksController(IDeadlockRepository dRepo, IMapper mapper)
        {
            _dRepository = dRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetDeadlocks()
        {
            var deadlocks = _dRepository.GetDeadlocks();
            var deadlocksDto = new List<DeadlockDto>();

            foreach (var deadlock in deadlocks)
            {
                deadlocksDto.Add(_mapper.Map<DeadlockDto>(deadlock));
            }

            return Ok(deadlocksDto);
        }

        [HttpGet("{deadlockId}", Name=nameof(GetDeadlock))]
        public IActionResult GetDeadlock(string deadlockId)
        {
            var deadlock = _dRepository.GetDeadlock(deadlockId);
            if(deadlock == null) return NotFound($"Deadlock with id {deadlockId} is not found!");

            var deadlockDto = _mapper.Map<DeadlockDto>(deadlock);

            return Ok(deadlockDto);
        }

        [HttpPost]
        public IActionResult AddDeadlock([FromBody] DeadlockDto deadlockDto)
        {
            var deadlock = _mapper.Map<Deadlock>(deadlockDto);

            var deadlockId = _dRepository.AddDeadlock(deadlock);
            if(deadlockId == null) return Conflict($"Deadlock with id {deadlockId} already exists!");
            
            return CreatedAtRoute(nameof(GetDeadlock), new { deadlockId = deadlockId }, deadlock);
        }

        [HttpPatch(Name = nameof(UpdateDeadlock))]
        public IActionResult UpdateDeadlock([FromBody] DeadlockDto deadlockDto)
        {
            var deadlock = _mapper.Map<Deadlock>(deadlockDto);
            _dRepository.UpdateDeadlock(deadlock);

            return NoContent();
        }

        [HttpDelete("{deadlockId}", Name = nameof(DeleteDeadlock))]
        public IActionResult DeleteDeadlock(string deadlockId)
        {
            var deadlockExists = _dRepository.DoesDeadlockExist(deadlockId);
            if(!deadlockExists) return NotFound($"Deadlock with id {deadlockId} is not found!");

            _dRepository.DeleteDeadlock(deadlockId);

            return NoContent();
        }
    }
}
