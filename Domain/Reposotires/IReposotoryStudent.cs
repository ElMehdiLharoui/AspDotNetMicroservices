using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Reposotires
{
    public interface IReposotoryStudent
    {
        Task<StudentDTO> GetStudentByIdAsync(int id, CancellationToken cancellationToken);
    }
}
