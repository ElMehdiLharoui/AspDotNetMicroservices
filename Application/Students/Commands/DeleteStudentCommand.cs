using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Students.Commands
{
    public class DeleteStudentCommand : IRequest<Result>
    {
        public string Id { get; set; }
    }
}
