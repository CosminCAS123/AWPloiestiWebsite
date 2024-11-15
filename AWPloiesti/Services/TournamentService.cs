using AWPloiesti.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace AWPloiesti.Services
{
    public class TournamentService : ITournamentService
    {
         private AWDbContext dbContext;


        private  int current_tournament_id;

        private int current_stage = 0;

        private readonly IUserService userService;

        private Tournament current_tournament;
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
            var pairs = new List<(string, string)>();
            var participantIds = await this.userService.GetParticipantsIdsAsync(current_tournament.TournamentID);
            if (stage == 1)
            {
                //get all ID's from participants within this tournament 
                 participantIds = await this.userService.GetParticipantsIdsAsync(current_tournament.TournamentID);

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

                

                for (int i = 0; i < participantIds.Count; i += 2)
                {
                    var first = await this.userService.GetByIdAsync(participantIds[i]);
                    var second = await this.userService.GetByIdAsync(participantIds[i + 1]);
                    pairs.Add((first.FullName , second.FullName));
                }

                return pairs;


            }

            List<int> winners = new List<int>();
            List<int> losers = new List<int>();

            //get winners(0L) , get losers(1L)

            winners = await this.userService.GetWinnersAsync(current_tournament.TournamentID);
            losers = await this.userService.GetLosersAsync(current_tournament.TournamentID);
            //ranadomize them
            var random2 = new Random();
            winners = winners.OrderBy(x => random2.Next()).ToList();
            losers = losers.OrderBy(x => random2.Next()).ToList();


            int? odd_loser_id = null, odd_winner_id = null;

            if (losers.Count % 2 == 1)
            {
                odd_loser_id = losers[losers.Count - 1];
                losers.RemoveAt(losers.Count - 1);
            }
            if (winners.Count % 2 == 1)
            {
                odd_winner_id = winners[winners.Count - 1];
                winners.RemoveAt(winners.Count - 1);
            }


            //add winner pairs
           
            for (int i = 0; i < winners.Count; i += 2)
            {
                var first_in_pair = await this.userService.GetByIdAsync(winners[i]);
                var second_in_pair = await this.userService.GetByIdAsync(winners[i+1]);
                pairs.Add((first_in_pair.FullName, second_in_pair.FullName));
            }
            //add losers pairs
            for (int i = 0; i < losers.Count; i += 2)
            {
                var first_in_pair = await this.userService.GetByIdAsync(losers[i]);
                var second_in_pair = await this.userService.GetByIdAsync(losers[i + 1]);
                pairs.Add((first_in_pair.FullName, second_in_pair.FullName));
            }


            //if both are odd , give them a match

            if (odd_loser_id is not null && odd_winner_id is not null)
            {
                var first = await this.userService.GetByIdAsync((int)odd_loser_id);
                var second = await this.userService.GetByIdAsync(((int)odd_winner_id));
                pairs.Add((first.FullName, second.FullName));
            }

            else if (odd_loser_id is not null)
            {
                var user = await this.userService.GetByIdAsync((int)odd_loser_id);
                pairs.Add((user.FullName, user.FullName));
            }
            else if (odd_winner_id is not null)
            {
                var user = await this.userService.GetByIdAsync((int)odd_winner_id);
                pairs.Add((user.FullName, user.FullName));
            }




            return pairs;



        }

        public async Task<Tournament> GetTournamentByIdAsync(int id)
        {
            return await this.dbContext.Tournaments.FirstAsync(u => u.TournamentID == id);
        }

      

       
   

        public async Task AddStageAsync()
        {
            
            current_tournament.CurrentStage++;
            await this.dbContext.SaveChangesAsync();
        }

        public int GetCurrentStage(int id)
        {
            return this.current_tournament.CurrentStage;
        }

        public async Task SetOwnTournamentAsync(int id)
        {
            this.current_tournament = await GetTournamentByIdAsync(id);
        }

     

       
    }
}
