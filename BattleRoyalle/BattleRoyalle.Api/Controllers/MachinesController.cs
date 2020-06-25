using BattleRoyalle.Domain.Commands.Input.Machine;
using BattleRoyalle.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BattleRoyalle.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachinesController : ControllerBase
    {
        private readonly IMachineService _machineService;

        public MachinesController(IMachineService machineService)
        {
            _machineService = machineService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var machines = _machineService.GetMachines();

                return Ok(machines);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{machineId}")]
        public IActionResult GetById(Guid machineId)
        {
            try
            {
                var machine = _machineService.GetMachine(id: machineId);

                return Ok(machine);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Save([FromBody]CreateMachineCommand command)
        {
            try
            {
                var result = _machineService.SaveMachine(command);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{machineId}")]
        public IActionResult Delete(Guid machineId)
        {
            try
            {
                var result = _machineService.DeleteMachine(machineId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}