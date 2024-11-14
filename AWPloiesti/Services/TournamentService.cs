using AWPloiesti.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace AWPloiesti.Services
{
    public class TournamentService : ITournamentService
    {
         private AWDbContext dbContext;


        private  int current_tournament_id;
        

        private readonly IUserService userService;
         public TournamentService(AWDbContext dbContext , IUserService service)
         {
            this.userService = service;
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

        public async Task<List<(string, string)>> GetAllMatchesForStageAsync(int stage)
        {
           if (stage == 1)
            {
                //get all ID's from participants within this tournament 
                var participantIds = await this.userService.GetParticipantsIdsAsync(GetCurrentTournamentID());

                var random = new Random();
                // Shuffle the list
                participantIds = participantIds.OrderBy(x => random.Next()).ToList();

                int? oddParticipant = null;

                // If the count is odd, remove the last participant and remember their ID
                if (participantIds.Count % 2 != 0)
                {
                    oddParticipant = participantIds[participantIds.Count - 1];
                    participantIds.RemoveAt(participantIds.Count - 1);
                }

                var pairs = new List<(string, string)>();

                for (int i = 0; i < participantIds.Count; i += 2)
                {
                    var first = await this.userService.GetByIdAsync(participantIds[i]);
                    var second = await this.userService.GetByIdAsync(participantIds[i + 1]);
                    pairs.Add((first.FullName , second.FullName));
                }

                return pairs;


            }
            throw new NotImplementedException();
        }

        public async Task<Tournament> GetTournamentByIdAsync(int id)
        {
            return await this.dbContext.Tournaments.FirstAsync(u => u.TournamentID == id);
        }

        public int GetCurrentTournamentID()
        {
            return this.current_tournament_id;
        }

        public void SetCurrentTournamentID(int id)
        {
            this.current_tournament_id = id;
        }
    }
}
