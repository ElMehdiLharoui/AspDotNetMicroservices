using Application.Students.Handler;
using Application.Students.Models;
using Application.Students.Querys;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Students.Models;

namespace Students.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class StudentControllerV2 : ControllerBase
    {
        public readonly IMediator Sender;
        public StudentControllerV2(IMediator sender)
        {
            Sender = sender;
        }


        public List<Student> Students = new List<Student>
        {
            new Student (),
            new Student () {Id=5,Name="mehdi",Description="test"},
            new Student () {Id=6,Name="mehdi2",Description="test2"}

        };
        [MapToApiVersion("2.0")]
        [HttpGet("GetAll")]
        public IActionResult getAnonym()
        {

            return Ok(new { id = 1, nom = "mehdi" });
        }
        [MapToApiVersion("2.0")]
        [HttpGet("{id:int}")]// Query
        [ProducesResponseType(typeof(StudentModel), 200)]
        public async Task<IActionResult> GetSingle( [FromRoute] int id, CancellationToken cancellationToken)
        {
            //return Ok(new { id = 1, nom = "mehdi" });
            // Preparation de Query 
            var quer = new GetStudentByIdQuery()
            {
                Id = id,
            };

            // 2 Appleler le handler 
            //   var hanler = new GetStudentByIdQueryHandler();
            // var result = await hanler.Handle(quer, cancellationToken);

            var resut = await Sender.Send(quer, cancellationToken) ;

            if(resut.IsFailed)
            { return Problem(detail: resut.Errors[0].Message); }
            //return Ok(Students.FirstOrDefault(s => s.Id==id));
            return Ok(resut.Value);

        }


    }
   
 }



    

