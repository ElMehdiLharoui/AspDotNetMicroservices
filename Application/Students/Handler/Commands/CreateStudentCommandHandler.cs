using Application.Students.Commands;
using Domain.Entities;
using Domain.Reposotires;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Students.Handler.Commands
{
    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, Result<string>>
    {
        private readonly IRepositoryStudent _repositoryStudent;

        public CreateStudentCommandHandler(IRepositoryStudent repositoryStudent)
        {
            _repositoryStudent = repositoryStudent;
        }

        public async Task<Result<string>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return Result.Fail<string>("Le nom de l'étudiant est requis.");
            }


            var newStudent = new StudentDTO
            {
                Name = request.Name,
                Description = request.Description,

            };


            var result = await _repositoryStudent.AddStudentAsync(newStudent, cancellationToken);

            if (result.IsSuccess)
            {
                return Result.Ok("L'étudiant a été créé avec succès.");
            }
            else
            {

                return Result.Fail<string>("Erreur lors de la création de l'étudiant.");
            }
        }
    }
}
