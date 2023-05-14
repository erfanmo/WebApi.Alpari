﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApi.Alpari.Models.Dto;
using WebApi.Alpari.Models.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Alpari.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoRepository _todoRepository;
        public TodoController(TodoRepository todoRepository)
        {
            _todoRepository=todoRepository;
        }
        // GET: api/<TodoController>
        [HttpGet]
        public IActionResult Get()
        {
            var todoList = _todoRepository.GetAll().Select(p=> new TodoItemDto
            {
                Id = p.Id,
                inserttime=p.InsertTime,
                Text=p.Tex
            }).ToList();
            return Ok(todoList);
        }

        // GET api/<TodoController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var todo = _todoRepository.Get(id);
            return Ok(new TodoItemDto
            {
                Id = todo.Id,
                Text = todo.Tex,
                inserttime = todo.InsertTime
            });
        }

        // POST api/<TodoController>
        [HttpPost]
        public IActionResult Post([FromBody] ItemDto item)
        {
            var result = _todoRepository.Add(new AddToDoDto
            {
                todo = new TodoDto 
                {
                    Tex = item.Text,
                }
            });
            string url = Url.Action(nameof(Get), "Todo", new { Id = result.todo.Id }, Request.Scheme);
            return Created(url, true);
        }

        // PUT api/<TodoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TodoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
