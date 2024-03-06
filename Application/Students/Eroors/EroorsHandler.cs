using Application.Students.Models;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Students.Eroors
{
    public class EroorsHandler : Error
    {
        public EroorsHandler(string message) : base(message)
        {

        }
        public static Result<T> HandleNegativeId<T>() 
        {
            string errorMessage = $"Erreur : L'ID est inférieur à 0. Traitement spécifique à implémenter.";
            return Result.Fail<T>(errorMessage);
        }
        public static Result HandleNotFoundId<T>()
        {
            string errorMessage = $"Erreur : L'ID est introuvable. Traitement spécifique à implémenter.";
            return Result.Fail(errorMessage);
        }
        public static Result HandleNegativeId()
        {
            string errorMessage = $"Erreur : L'ID est inférieur à 0. Traitement spécifique à implémenter.";
            return Result.Fail(errorMessage);
        }

    }
}
