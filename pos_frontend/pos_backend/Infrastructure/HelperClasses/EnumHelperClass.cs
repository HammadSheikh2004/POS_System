using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.HelperClasses
{
    public static class EnumHelperClass 
    {
        public static List<object> EnumList<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<T>().Select(e => new { Id = Convert.ToInt32(e), Name = e.ToString() }).ToList<object>();
        }
    }
}
