using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blf2.Net8.Entitry.ResponseType {
    public static class PagedExtensions {
        public static PagedList<T> ToPagedList<T>(this IQueryable<T> query, int pageNumber, int pageSize){
            var count = query.Count();
            var items = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
