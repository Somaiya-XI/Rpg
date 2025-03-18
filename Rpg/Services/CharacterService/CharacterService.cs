using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Rpg.Dtos.Skill;

namespace Rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {

        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User.
        FindFirstValue(ClaimTypes.NameIdentifier)!);

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();

            var character = _mapper.Map<Character>(newCharacter);

            character.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());

            _context.Characters.Add(character);
            await _context.SaveChangesAsync();

            response.Data =
            await _context.Characters
            .Where(u => u.User!.Id == GetUserId())
            .Select(c => _mapper.Map<GetCharacterDto>(c))
            .ToListAsync();

            return response;

        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();

            try
            {
                var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id && c.User!.Id == GetUserId());

                if (dbCharacter is null)
                    throw new Exception($"Character with Id '{id}' not found.");

                _context.Characters.Remove(dbCharacter);

                await _context.SaveChangesAsync();

                response.Data = await _context.Characters
                .Where(c => c.User!.Id == GetUserId())
                .Select(c => _mapper.Map<GetCharacterDto>(c))
                .ToListAsync();

            }
            catch (Exception ex)
            {
                response.IsOk = false;
                response.Message = ex.Message;

            }
            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _context.Characters
            .Include(c => c.Weapon)
            .Include(c => c.Skills)
            .Where(u => u.User!.Id == GetUserId()).ToListAsync();
            response.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();

            return response;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var response = new ServiceResponse<GetCharacterDto>();

            try
            {

                var dbCharacter =
                await _context.Characters
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .FirstOrDefaultAsync
                (c => c.Id == id && c.User!.Id == GetUserId());

                if (dbCharacter is null)
                    throw new Exception($"Character with Id '{id}' not found.");

                response.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
            }
            catch (Exception ex)
            {
                response.IsOk = false;
                response.Message = ex.Message;
            }
            return response;


        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            var response = new ServiceResponse<GetCharacterDto>();

            try
            {
                var dbCharacter = await _context.Characters
                .Include(c => c.User) //eager loading 
                .FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);

                if (dbCharacter is null || dbCharacter.User!.Id != GetUserId())
                    throw new Exception($"Character with Id '{updatedCharacter.Id}' not found.");

                _mapper.Map(updatedCharacter, dbCharacter);

                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetCharacterDto>(dbCharacter);

            }
            catch (Exception ex)
            {
                response.IsOk = false;
                response.Message = ex.Message;

            }
            return response;

        }

        public async Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            try
            {
                var character = await _context.Characters
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .FirstOrDefaultAsync(
                    c => c.Id == newCharacterSkill.CharacterId
                    && c.User!.Id == GetUserId()
                );
                if (character is null)
                    throw new Exception($"Character with Id '{newCharacterSkill.CharacterId}' not found.");


                var skill = await _context.Skills.FirstOrDefaultAsync(s => s.Id == newCharacterSkill.SkillId);
                if (skill is null)
                    throw new Exception($"Skill with Id '{newCharacterSkill.SkillId}' not found.");

                character.Skills!.Add(skill);
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetCharacterDto>(character);

            }
            catch (Exception ex)
            {

                response.IsOk = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}