using System.Collections.Generic;
using System.Linq;
using WebApi.Alpari.Models.Context;
using WebApi.Alpari.Models.Entities;

namespace WebApi.Alpari.Models.Services
{
    public class CategoryRepository
    {
        private readonly DataBaseContext _context;
        public CategoryRepository(DataBaseContext context)
        {
            _context = context;
        }

        public List<CategoryDto> Get()
        {
           var data = _context.Category.ToList()
                .Select(p => new CategoryDto
                {
                    Id = p.Id,
                    Name = p.Name,
                }).ToList();

            return data;
        }

        public CategoryDto Find(int id)
        {
            var category = _context.Category.Find(id);
            return (new CategoryDto
            {
                Name = category.Name,
            });
        }

        public int AddCategory(string Name)
        {
            Category category = new Category()
            { Name = Name };
            _context.Category.Add(category);
            _context.SaveChanges();
            return  category.Id;
        }

        public int Delete(int id)
        {
            _context.Category.Remove(new Category { Id = id });
            return _context.SaveChanges();
        }

        public int Edit(CategoryDto categorydto)
        {
            var category = _context.Category.FirstOrDefault(p => p.Id == categorydto.Id);
            category.Name = categorydto.Name;
            return _context.SaveChanges();
        }
    }


    public class CategoryDto
    {
        public int  Id { get; set; }
        public string Name { get; set; }
    }
}
