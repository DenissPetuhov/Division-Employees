using Domain.Dto;
using Domain.Entity;
using Domain.Interface.Services;
using Domain.Result;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DivisionController : Controller
    {
        private readonly IDivisionService _divisionService;

        public DivisionController(IDivisionService divisionService)
        {
            _divisionService = divisionService;
        }

        [HttpGet("get-division-by-id id={divisionId}")]
        public async Task<ActionResult<BaseResult<Division>>> GetDivsionAsync(int divisionId)
        {
            var response = await _divisionService.GetDivisionAsync(divisionId);
            if (response.isSuccses) return Ok(response);

            return BadRequest(response);
        }

        [HttpPost("add-parent-division")]
        public async Task<ActionResult<BaseResult>> AddParentDivisionAsync(AddParentDivisionDto divisionDto)
        {
            var response = await _divisionService.AddParentDivision(divisionDto);
            if (response.isSuccses) return Ok(response);

            return BadRequest(response);
        }
        [HttpGet("get-all-division")]
        public async Task<ActionResult<CollectionResult<Division>>> GetAllDivision()
        {
            var response = await _divisionService.GetAllDivisionsAsync();
            if (response.isSuccses) return Ok(response);

            return BadRequest(response);
        }

        [HttpPost("create-division")]
        public async Task<ActionResult<BaseResult>> CreateDivision(CreateDivisionDto divisionDto)
        {
            var response = await _divisionService.CreateDivisionAsync(divisionDto);
            if (response.isSuccses) return Ok(response);

            return BadRequest(response);
        }

        [HttpPut("update-division")]
        public async Task<ActionResult<BaseResult>> UpdateDivision(DivisionDto divisionDto)
        {
            var response = await _divisionService.UpdateDivisionAsync(divisionDto);
            if (response.isSuccses) return Ok(response);

            return BadRequest(response);
        }

        [HttpDelete("delete-division-by-id={id}")]
        public async Task<ActionResult<BaseResult>> DeleteDivision(int id)
        {
            var response = await _divisionService.DeleteDivisionAsync(id);
            if (response.isSuccses) return Ok(response);

            return BadRequest(response);
        }


    }
}
