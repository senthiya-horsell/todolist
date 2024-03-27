using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TodoListApi.Models;

namespace TodoListApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private static readonly List<TodoItem> _todoItems = new List<TodoItem>();
        private static int _nextId = 1;

        // GET: api/todo
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_todoItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/todo/5
        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById(int id)
        {
            try
            {
                var todo = _todoItems.Find(t => t.Id == id);
                if (todo == null)
                {
                    return NotFound("Item not found");
                }
                return Ok(todo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/todo
        [HttpPost]
        public IActionResult Post([FromBody] TodoItem item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("Invalid data");
                }
                // Assign the current value of _nextId to the item's Id
                item.Id = _nextId++;
                // Add the item to the list
                _todoItems.Add(item);
                // Log the state of _todoItems list after adding the item
                Console.WriteLine($"TodoItem added: {item.Task}");
                return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/todo/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var todo = _todoItems.Find(t => t.Id == id);
                if (todo == null)
                {
                    return NotFound("Item not found");
                }
                _todoItems.Remove(todo);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
