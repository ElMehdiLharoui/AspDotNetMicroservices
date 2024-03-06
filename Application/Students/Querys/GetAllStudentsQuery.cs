using Application.Students.Models;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Students.Querys
{
    public class GetAllStudentsQuery : IRequest<Result<List<StudentModel>>>
    {
        
    }
}
