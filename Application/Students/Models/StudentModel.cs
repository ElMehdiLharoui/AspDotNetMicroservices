using Application.Common.Mappings;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Students.Models
{
    public class StudentModel : IMapFrom<StudentDTO>
    {
        public string? Id { get; set; }
        public string Name { get; set; } = "ElMehdi";
        public string Description { get; set; } = string.Empty;
    }
}
