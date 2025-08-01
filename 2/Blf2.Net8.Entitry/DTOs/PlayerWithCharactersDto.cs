using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blf2.Net8.Entitry.DTOs {
    public class PlayerWithCharactersDto : PlayerDto{
        public IEnumerable<CharacterDto> Characters { get; set; } 
    }
} 
