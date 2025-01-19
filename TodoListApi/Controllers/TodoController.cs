using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TodoListApi.Models;
using TodoListApi.Repositories;

namespace TodoListApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoRepository _repository;

        public TodoController(TodoRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Todo
        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> GetAll()
        {
            return Ok(_repository.GetAll());
        }

        // POST: api/Todo
        [HttpPost]
        public ActionResult<TodoItem> Add([FromBody] TodoItem item)
        {
            if (item == null)
            {
                return BadRequest("Item is null");
            }

            var id = _repository.Add(item);
            return CreatedAtAction(nameof(GetAll), new { id = id }, item);
        }

        // PUT: api/Todo/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] TodoItem item)
        {
            if (item == null || id != item.Id)
            {
                return BadRequest("Invalid data");
            }

            var updatedItem = _repository.Update(item);
            return Ok(updatedItem);
        }
        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.Delete(id);
            return Ok();
        }
    }
}