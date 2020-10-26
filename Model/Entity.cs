using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 父类
    /// </summary>
    public class Entity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("n");
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
    }
}
