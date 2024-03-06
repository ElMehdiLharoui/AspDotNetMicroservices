using Application.Students.Commands;
using Application.Students.Handler;
using Application.Students.Models;
using Application.Students.Querys;
using Domain.Entities;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Students.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        public readonly IMediator Sender;
        public StudentController(IMediator sender)
        {
            Sender = sender;
        }


        [HttpGet("{id}")]// Query
        [ProducesResponseType(typeof(StudentModel), 200)]
        public async Task<IActionResult> GetSingle([FromRoute] string id, CancellationToken cancellationToken)
        {
            // Preparation de Query 
            var quer = new GetStudentByIdQuery()
            {
                Id = id,
            };

            var resut = await Sender.Send(quer, cancellationToken);

            if (resut.IsFailed)
            { 
                return Problem(detail: string.Join(" ,",resut.Errors.ConvertAll(x=> x.Message)));
            }
            
            return Ok(resut.Value);
        }
        [HttpPost]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> CreateStudent([FromBody] StudentModel model, CancellationToken cancellationToken)
        {
            var command = new CreateStudentCommand
            {
                Name = model.Name,
                Description = model.Description
            };

            var result = await Sender.Send(command, cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllStudents(CancellationToken cancellationToken)
        {
            var query = new GetAllStudentsQuery();
            var result = await Sender.Send(query,cancellationToken) ;

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(string id,CancellationToken cancellationToken)
        {
            var command = new DeleteStudentCommand
            {
                Id = id
            };

            var result = await Sender.Send(command,cancellationToken);
           
            if (result.IsSuccess)
            {
                return NoContent(); 
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

    }
}





