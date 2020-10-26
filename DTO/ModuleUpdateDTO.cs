using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    /// <summary>
    /// 用于module修改界面的展示
    /// </summary>
    public class ModuleUpdateDTO
    {
        /// <summary>
        /// 要修改的module
        /// </summary>
        public ModuleDTO ModuleDTO { get; set; }
        /// <summary>
        /// controller对应的module集合
        /// </summary>
        public List<ModuleDTO> ModuleDTOs { get; set; }
    }
}
