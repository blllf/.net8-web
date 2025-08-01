using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blf2.Net8.Entitry {
    public class Character {
        public Guid Id { get; set; }
        public string NickName { get; set; }
        public string Classes { get; set; }
        public int Level { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid PlayerId { get; set; }
        public Player Player { get; set; }

    }
}
