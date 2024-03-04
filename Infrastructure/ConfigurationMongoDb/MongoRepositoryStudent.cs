using Domain.Entities;
using Domain.Reposotires;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class MongoRepositoryStudent : IReposotoryStudent
    {
        private readonly IMongoCollection<StudentDTO> _studentsCollection;
        public MongoRepositoryStudent(IMongoDatabase database)
        {
            _studentsCollection = database.GetCollection<StudentDTO>("students");
        }
        async Task<StudentDTO> IReposotoryStudent.GetStudentByIdAsync(int id, CancellationToken cancellationToken)
        {
            var filter = Builders<StudentDTO>.Filter.Eq(s => s.Id, id);
            return await _studentsCollection.Find(filter).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
