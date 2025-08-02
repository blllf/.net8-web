using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blf2.Net8.Entitry.ResponseType {
    public class PagedList<T> : List<T>{
        public PagedMetaData pagedMetaData { get; set; }
        public PagedList(List<T> items, int count, int pageNumber, int pageSize) {
            pagedMetaData = new PagedMetaData {
                TotalCount = count, 
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };
            AddRange(items); //将items集合中的所有元素添加到当前集合中
        }
    }
}
