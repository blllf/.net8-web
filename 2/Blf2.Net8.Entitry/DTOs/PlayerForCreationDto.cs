using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blf2.Net8.Entitry.DTOs {
    public class PlayerForCreationDto {
        [Required(ErrorMessage = "账号不能为空")]
        [StringLength(20 , ErrorMessage = "长度不能大于20")]
        public string Account {  get; set; }

        [Required(ErrorMessage = "账号类型不能为空")]
        [StringLength(10, ErrorMessage = "长度不能大于10")]
        public string AccountType { get; set; }
    }
}
