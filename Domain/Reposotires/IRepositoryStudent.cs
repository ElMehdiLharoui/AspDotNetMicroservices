using Domain.Entities;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.Reposotires
{
    public interface IRepositoryStudent
    {
        Task<StudentDTO> GetStudentByIdAsync(string id, CancellationToken cancellationToken);
        Task<Result> AddStudentAsync(StudentDTO student, CancellationToken cancellationToken);
        Task<List<StudentDTO>> GetAllStudentsAsync(CancellationToken cancellationToken);
        Task<Result> DeleteStudentByIdAsync(string id, CancellationToken cancellationToken);
    }
}
