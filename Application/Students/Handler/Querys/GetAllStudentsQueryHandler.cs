using Application.Students.Models;
using Application.Students.Querys;
using AutoMapper;
using Domain.Reposotires;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Students.Handler.Querys
{
    public class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQuery, Result<List<StudentModel>>>
    {
        private readonly IRepositoryStudent _repositoryStudent;
        private readonly IMapper _mapper;

        public GetAllStudentsQueryHandler(IRepositoryStudent repositoryStudent, IMapper mapper)
        {
            _repositoryStudent = repositoryStudent;
            _mapper = mapper;
        }

        public async Task<Result<List<StudentModel>>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            var students = await _repositoryStudent.GetAllStudentsAsync(cancellationToken);

            var studentList = _mapper.Map<List<StudentModel>>(students);

            return Result.Ok(studentList);
        }
    }
}
