using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    /// <summary>
    /// webapi的用户修改方法的参数对象
    /// </summary>
    public class UserUpdateDTO
    {
        public UserDTO UserDTO { get; set; }
        public string[] Role_Ids { get; set; }
    }
}
