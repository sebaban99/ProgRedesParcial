using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Iemedebe.Domain;

namespace Iemedebe.BusinessLogic
{
    public interface IRatingLogic<T> : ILogic<T>
    {
        Task<Rating> UpdateRatingAsync(Guid idRating, int score);
    }
}
