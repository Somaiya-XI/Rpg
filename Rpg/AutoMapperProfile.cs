using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rpg.Dtos.Skill;
using Rpg.Dtos.Weapon;

namespace Rpg
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>();
            CreateMap<AddCharacterDto, Character>();
            CreateMap<UpdateCharacterDto, Character>();
            CreateMap<AddWeaponDto, Weapon>();
            CreateMap<Weapon, GetWeaponDto>();
            CreateMap<Skill, GetSkillDto>();

        }
    }
}