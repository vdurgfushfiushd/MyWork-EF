using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper
{
    public class FangJiaPageHelper<T> where T:Entity
    {
        //当前页码
        public int CurrentPage { get; set; }
        //总页码
        public int PageCount { get; set; }
        //每页大小
        public int PageSize { get; set; }
        //mvc分页

    }
}
