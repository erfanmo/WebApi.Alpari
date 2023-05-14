using System;
using System.Collections.Generic;

namespace WebApi.Alpari.Models.Entities
{
    public class ToDo
    {
        public int Id { get; set; }
        public string Tex { get; set; }
        public DateTime InsertTime { set; get; }
        public bool IsDeleted { get; set; }
        public ICollection <Category> Categories { get; set; }
    }


    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ToDo> Todos { get; set; }
    }
}
