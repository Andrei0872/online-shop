using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using server.DAL.GenericRepository;
using server.Data;
using server.Models;
using Microsoft.EntityFrameworkCore;


namespace server.Repositories.SessionRepository
{
    public class SessionRepository : GenericRepository<SessionToken>, ISessionRepository
    {
    public SessionRepository(ServerContext serverContext) : base(serverContext) {}

    public async Task<SessionToken> GetByJTI(string JTI)
    {
        return await this._serverContext.SessionTokens
            .FirstOrDefaultAsync(t => t.Jti.Equals(JTI));
    }
    }
}