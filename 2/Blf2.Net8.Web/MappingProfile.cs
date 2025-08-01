using AutoMapper;
using Blf2.Net8.Entitry;
using Blf2.Net8.Entitry.DTOs;

namespace Blf2.Net8.Web {
    /// <summary>
    /// 映射配置类
    /// </summary>
    public class MappingProfile : Profile{
        //创建映射关系
        public MappingProfile() {
            CreateMap<Player, PlayerDto>();
            //PlayerWithCharactersDto 中多出的属性也会被自动映射，前提是源对象 Player 中存在同名属性
            CreateMap<Player, PlayerWithCharactersDto>();
            CreateMap<Character, CharacterDto>();

            CreateMap<PlayerForCreationDto, Player>();
            CreateMap<PlayerForUpdateDto, Player>();
        }
    }
}
