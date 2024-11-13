using AWPloiesti.Models;
using Microsoft.AspNetCore.Components;

namespace AWPloiesti.Services
{
    public class TournamentService : ITournamentService
    {
         private AWDbContext dbContext;
         public TournamentService(AWDbContext dbContext)
         {
                   this.dbContext = dbContext;
         }
        public async Task<OperationResult> AddTournamentAsync(Tournament tournament)
        {
            try
            {
                await this.dbContext.Tournaments.AddAsync(tournament);
                await this.dbContext.SaveChangesAsync();
                return new OperationResult { Message = "Turneul a fost adaugat!", Success = true };
            }
            catch
            {
                return new OperationResult { Message = "Eroare la crearea turneului" , Success = true };
            }
        }
    }
}
