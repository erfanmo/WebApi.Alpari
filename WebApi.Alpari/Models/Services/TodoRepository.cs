using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Alpari.Models.Context;
using WebApi.Alpari.Models.Entities;

namespace WebApi.Alpari.Models.Services
{
    public class TodoRepository
    {
        private readonly DataBaseContext _context;
        public TodoRepository(DataBaseContext context)
        {
            _context = context;
        }

        public List<TodoDto> GetAll()
        {
            return _context.Todo.Select(p=> new TodoDto
            {
                Id=p.Id,
                InsertTime=p.InsertTime,
                IsDeleted=p.IsDeleted,
                Tex=p.Tex
            }).ToList();
        }

        public TodoDto Get(int id)
        {
            var todo = _context.Todo.FirstOrDefault(p => p.Id == id);
            return new TodoDto
            {
                Id = todo.Id,
                InsertTime = todo.InsertTime,
                Tex = todo.Tex,
                IsDeleted = todo.IsDeleted,
                
            };
        }

        public AddToDoDto Add(AddToDoDto todo)
        {
            ToDo newTodo = new ToDo()
            {
                Id = todo.todo.Id,
                InsertTime = DateTime.Now,
                IsDeleted = false,
                Tex = todo.todo.Tex,
            };
            foreach (var item in todo.Categories)
            {
                var category = _context.Category.SingleOrDefault(p => p.Id == item);
                newTodo.Categories.Add(category);
            }
            _context.Todo.Add(newTodo);
            _context.SaveChanges();
            return new AddToDoDto
            {
                todo = new TodoDto
                {
                    Id = newTodo.Id,
                    InsertTime = newTodo.InsertTime,
                    IsDeleted = newTodo.IsDeleted,
                    Tex = newTodo.Tex,
                }
                ,
                Categories = todo.Categories
            };
        }

        public void Delete(int id)
        {
            var todo = _context.Todo.Find(id);
            todo.IsDeleted = false;
            _context.SaveChanges();
        }

        public bool Edit(EditToDoDto edit)
        {
            var todo = _context.Todo.SingleOrDefault(p => p.Id == edit.Id);
            todo.Tex = edit.Text;
            _context.SaveChanges();
            return true;
        }
    }

    public class TodoDto
    {
        public int Id { get; set; }
        public string Tex { get; set; }
        public DateTime InsertTime { set; get; }
        public bool IsDeleted { get; set; }
    }

    public class AddToDoDto
    {
        public TodoDto todo { get; set; }
        public List<int> Categories { get; set; } = new List<int>();
    }

    public class EditToDoDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<int> Categories { get; set; }
    }
}
