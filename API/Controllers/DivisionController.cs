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

        [HttpGet("get-by-id")]
        public async Task<ActionResult<BaseResult<Division>>> GetDivsionAsync(int divisionId)
        {
            var response =  _divisionService.GetDivision(divisionId);
            if (response.isSuccses) return Ok(response);

            return BadRequest(response);
        }

        [HttpPost("add-parent-division")]
        public async Task<ActionResult<BaseResult>> AddParentDivisionAsync(AddParentDivisionDto divisionDto)
        {
            var response = await _divisionService.AddParentDivisionAsync(divisionDto);
            if (response.isSuccses) return Ok(response);

            return BadRequest(response);
        }
        [HttpGet("get-all")]
        public async Task<ActionResult<CollectionResult<Division>>> GetAllDivisionAsync()
        {
            var response =  _divisionService.GetAllDivisions();
            if (response.isSuccses) return Ok(response);

            return BadRequest(response);
        }

        [HttpPost("create")]
        public async Task<ActionResult<BaseResult>> CreateDivisionAsync(CreateDivisionDto divisionDto)
        {
            var response = await _divisionService.CreateDivisionAsync(divisionDto);
            if (response.isSuccses) return Ok(response);

            return BadRequest(response);
        }

        [HttpPut("update")]
        public async Task<ActionResult<BaseResult>> UpdateDivisionAsync(DivisionDto divisionDto)
        {
            var response = await _divisionService.UpdateDivisionAsync(divisionDto);
            if (response.isSuccses) return Ok(response);

            return BadRequest(response);
        }

        [HttpDelete("delete-by-id")]
        public async Task<ActionResult<BaseResult>> DeleteDivisionAsync(int id)
        {
            var response = await _divisionService.DeleteDivisionAsync(id);
            if (response.isSuccses) return Ok(response);

            return BadRequest(response);
        }


    }
}
