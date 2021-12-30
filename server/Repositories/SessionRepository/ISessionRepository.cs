using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using server.Models;
using server.DAL.GenericRepository;

namespace server.Repositories.SessionRepository
{
    public interface ISessionRepository : IGenericRepository<SessionToken>
    {
        Task<SessionToken> GetByJTI(string JTI);
    }
}