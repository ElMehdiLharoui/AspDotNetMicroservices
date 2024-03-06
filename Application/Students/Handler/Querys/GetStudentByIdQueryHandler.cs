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


namespace Application.Students.Handler.Querys
{
    public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, Result<StudentModel>>
    {
        // private IReposotoryStudent reposotoryStudent;

        //public GetStudentByIdQueryHandler(IReposotoryStudent reposotory)
        //{
        //    //reposotoryStudent = reposotory;
        //}
        private readonly IRepositoryStudent _repositoryStudent;
        private readonly IMapper _mapper;
        public GetStudentByIdQueryHandler(IRepositoryStudent repositoryStudent, IMapper mapper)
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
            if (string.IsNullOrWhiteSpace(request.Id))
                return EroorsHandler.HandleNegativeId<StudentModel>();

            var studentDtoResult = await _repositoryStudent.GetStudentByIdAsync(request.Id, cancellationToken);

            if (studentDtoResult == null)
            {

                Console.WriteLine("Repository returned null for student ID: " + request.Id);
                return Result.Fail<StudentModel>("Student not found");
            }

            var studentModel = _mapper.Map<StudentModel>(studentDtoResult);

            if (studentModel == null)
            {

                Console.WriteLine("Mapping failed for student ID: " + request.Id);
                return Result.Fail<StudentModel>("Mapping failed");
            }

            return Result.Ok(studentModel);
        }



    }
}
