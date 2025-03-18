using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rpg.Dtos.Skill;

namespace Rpg.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly ICharacterService _characterService;


        public CharactersController(ICharacterService characterService)
        {
            _characterService = characterService;

        }


        [HttpGet("getAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get()
        {
            //return Ok() || BadRequest() || NotFound()
            return Ok(await _characterService.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetSingle(int id)
        {
            var resp = await _characterService.GetCharacterById(id);
            if (!resp.IsOk)
                return NotFound(resp);
            return Ok(resp);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto newCharacter)
        {
            return Ok(await _characterService.AddCharacter(newCharacter));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            var resp = await _characterService.UpdateCharacter(updatedCharacter);
            if (!resp.IsOk)
                return NotFound(resp);
            return Ok(resp);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> DeleteCharacter(int id)
        {
            var resp = await _characterService.DeleteCharacter(id);
            if (!resp.IsOk)
                return NotFound(resp);
            return Ok(resp);
        }

        [HttpPost("Skill")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> AddSkill(AddCharacterSkillDto newSkill)
        {
            var resp = await _characterService.AddCharacterSkill(newSkill);
            if (!resp.IsOk)
                return NotFound(resp);
            return Ok(resp);
        }
    }
}