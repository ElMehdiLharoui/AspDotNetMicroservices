using Amazon.Runtime.Internal;
using Domain.Entities;
using Domain.Reposotires;
using FluentResults;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MongoRepositoryStudent : IRepositoryStudent
    {
        private readonly IMongoCollection<StudentDTO> _studentsCollection;
        public MongoRepositoryStudent(IMongoDatabase database)
        {
            _studentsCollection = database.GetCollection<StudentDTO>("students");
        }
        public async Task<StudentDTO> GetStudentByIdAsync(string id, CancellationToken cancellationToken)
        {
            return (await _studentsCollection.FindAsync(x=> true)).FirstOrDefault(cancellationToken);
        }
        public async Task<Result> AddStudentAsync(StudentDTO student, CancellationToken cancellationToken)
        {
            try
            {
                await _studentsCollection.InsertOneAsync(student, null, cancellationToken);
                return Result.Ok();
            }
            catch (Exception ex)
            {
            
                return Result.Fail($"Erreur lors de l'ajout de l'étudiant : {ex.Message}");
            }
        }
        public async Task<List<StudentDTO>> GetAllStudentsAsync(CancellationToken cancellationToken)
        {
            var students = await _studentsCollection.Find(_ => true).ToListAsync(cancellationToken);
            return students;
        }
        public async Task<Result> DeleteStudentByIdAsync(string id, CancellationToken cancellationToken)
        {
            try
            {
                var deleteResult = await _studentsCollection.DeleteOneAsync(s => s.Id == ObjectId.Parse(id), cancellationToken);

                if (deleteResult.DeletedCount > 0)
                {
                    return Result.Ok();
                }
                else
                {
                    // Utilisez la méthode HandleNotFoundId de EroorsHandler pour gérer le cas où aucun étudiant n'a été supprimé
                    return EroorsHandler.HandleNotFoundId<Result>();
                }
            }
            catch (Exception ex)
            {
                return Result.Fail($"Erreur lors de la suppression de l'étudiant : {ex.Message}");
            }
        }
    }
}
