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

        [HttpGet("{id}")]
        public ActionResult<BaseResult<Division>> Get(int id)
        {
            var response = _divisionService.GetDivision(id);
            if (response.isSuccess) return Ok(response);
            return BadRequest(response);
        }

        [HttpGet]
        public ActionResult<CollectionResult<Division>> GetAll()
        {
            var response = _divisionService.GetAllDivisions();
            if (response.isSuccess) return Ok(response);
            return BadRequest(response);
        }

        [HttpPost]
        public async Task<ActionResult<BaseResult>> CreateDivisionAsync(CreateDivisionDto divisionDto)
        {
            var response = await _divisionService.CreateDivisionAsync(divisionDto);
            if (response.isSuccess) return Ok(response);
            return BadRequest(response);
        }

        [HttpPut]
        public async Task<ActionResult<BaseResult>> UpdateDivisionAsync(DivisionDto divisionDto)
        {
            var response = await _divisionService.UpdateDivisionAsync(divisionDto);
            if (response.isSuccess) return Ok(response);
            return BadRequest(response);
        }

        [HttpDelete]
        public async Task<ActionResult<BaseResult>> DeleteDivisionAsync(int id)
        {
            var response = await _divisionService.DeleteDivisionAsync(id);
            if (response.isSuccess) return Ok(response);
            return BadRequest(response);
        }

        [HttpGet("get-flat")]
        public ActionResult<CollectionResult<Division>> GetFlat(int? checkDivisionId)
        {
            var response = _divisionService.GetAllFlatDivisions(checkDivisionId);
            if (response.isSuccess) return Ok(response);
            return BadRequest(response);
        }
    }
}
