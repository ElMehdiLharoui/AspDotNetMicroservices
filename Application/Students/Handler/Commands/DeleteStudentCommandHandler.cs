using Application.Students.Commands;
using Application.Students.Eroors;
using Application.Students.Models;
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
    public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, Result>
    {
        private readonly IRepositoryStudent _repositoryStudent;

        public DeleteStudentCommandHandler(IRepositoryStudent repositoryStudent)
        {
            _repositoryStudent = repositoryStudent;
        }

        public async Task<Result> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Id))
            {
                return EroorsHandler.HandleNegativeId();
            }

            var result = await _repositoryStudent.DeleteStudentByIdAsync(request.Id, cancellationToken);

            if (result.IsSuccess)
            {
                return Result.Ok();
            }
            else
            {
                return Result.Fail("Erreur lors de la suppression de l'étudiant.");
            }
        }


    }
}
