using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Rpg.Dtos.Weapon;

namespace Rpg.Services.WeaponService
{
    public class WeaponService : IWeaponService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WeaponService(DataContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User.
        FindFirstValue(ClaimTypes.NameIdentifier)!);

        public async Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            try
            {
                //Check character existence && if it is for the current authenticated user
                var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == newWeapon.CharacterId && c.User!.Id == GetUserId());

                if (character is null)
                    throw new Exception($"Character with Id '{newWeapon.CharacterId}' does not exist ");

                //Auto map the upcoming Weapon(DTO) to the Weapon Model
                var weapon = _mapper.Map<Weapon>(newWeapon);
                
                // //Auto map the upcoming Weapon(DTO) to the Weapon Model
                // weapon.Character = character;

                _context.Weapons.Add(weapon);
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