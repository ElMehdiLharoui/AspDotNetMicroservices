using Amazon.Runtime.Internal;
using Application.Students.Eroors;
using Application.Students.Models;
using Application.Students.Querys;
using AutoMapper;
using Domain.Entities;
using Domain.Reposotires;
using FluentResults;
using MediatR;
using Microsoft.Extensions.DependencyInjection;


namespace Application.Students.Handler
{
    public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery,Result<StudentModel>>
    {
        // private IReposotoryStudent reposotoryStudent;

        //public GetStudentByIdQueryHandler(IReposotoryStudent reposotory)
        //{
        //    //reposotoryStudent = reposotory;
        //}
        private readonly IReposotoryStudent _repositoryStudent;
        private readonly IMapper _mapper;
        public GetStudentByIdQueryHandler(IReposotoryStudent repositoryStudent, IMapper mapper)
        {
            _repositoryStudent = repositoryStudent;
            _mapper = mapper;
        }

        //public async Task<Result<StudentModel>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        //{

        //    if (request.Id <= 0)
        //    {
        //        Result<string> errorResult = EroorsHandler.HandleNegativeId();
        //        return Result.Fail<StudentModel>(errorResult.Errors.First().Message);
        //    }
        //  //  var resutl = await reposotoryStudent.GetStudentByIdAsync(request.Id, cancellationToken);
        //        return Result.Ok(new StudentModel { Name = "Hamza" });

        //}
        public async Task<Result<StudentModel>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id <= 0)
            {
                Result<string> errorResult = EroorsHandler.HandleNegativeId();
                return Result.Fail<StudentModel>(errorResult.Errors.First().Message);
            }

            var studentDtoResult = await _repositoryStudent.GetStudentByIdAsync(request.Id, cancellationToken);


            var studentDto = studentDtoResult.ValueOrDefault(); 

            var studentModel = _mapper.Map<StudentModel>(studentDto);

            return Result.Ok(studentModel);
        }


    }
}
