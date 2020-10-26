using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class Tool<T> where T : Entity
    {
        public static Expression<Func<T, bool>> ToExpression(Dictionary<string, object> dictionary)
        {
            Expression final = null;
            ParameterExpression pe = Expression.Parameter(typeof(T), "e");
            foreach (var key in dictionary.Keys)
            {
                if (key.Contains("From"))  //大于或等于
                {
                    var _key = key.Replace("To", "");
                    //属性的数据类型
                    var key_type = typeof(T).GetProperty(_key).PropertyType;
                    Expression left = Expression.Property(pe, _key);
                    Expression right = Expression.Constant(dictionary[key], key_type);
                    final = Expression.GreaterThanOrEqual(left, right);
                }
                else if (key.Contains("To")) //小与或等于
                {
                    var _key = key.Replace("To","");
                    //属性的数据类型
                    var key_type = typeof(T).GetProperty(_key).PropertyType;
                    Expression left = Expression.Property(pe, _key);
                    Expression right = Expression.Constant(dictionary[key], key_type);
                    final = Expression.LessThanOrEqual(left, right);
                }
                else  //相等
                {
                    //属性的数据类型
                    var key_type = typeof(T).GetProperty(key).PropertyType;
                    Expression left = Expression.Property(pe, key);
                    Expression right = Expression.Constant(dictionary[key], key_type);
                    final = Expression.Equal(left, right);
                }            
            }
            var result = Expression.Lambda<Func<T, bool>>(final, new ParameterExpression[] { pe });
            return result;
        }

    }
}
