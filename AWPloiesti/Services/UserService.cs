using AWPloiesti.Models;
using Microsoft.EntityFrameworkCore;

namespace AWPloiesti.Services
{
    public class UserService : IUserService
    {
        private AWDbContext dbContext;
        public UserService(AWDbContext _dbcontext) => this.dbContext = _dbcontext;
        public async Task<OperationResult> AddParticipantsAsync(List<Participant> participants , int tournamentId)
        {
            try
            {
                foreach(Participant participant in participants)
                {
                    participant.TournamentID = tournamentId;
                    await this.dbContext.Participants.AddAsync(participant);
                    await this.dbContext.SaveChangesAsync();
                }
                return new OperationResult { Message = "Participantii au fost adaugati!", Success = true };
            }
            catch
            {
                return new OperationResult { Message = "Eroare la adaugarea participantilor", Success = false };
            }
        }

        public async Task<Participant?> GetByIdAsync(int id)
        {
            return await this.dbContext.Participants.FirstOrDefaultAsync(u => u.ParticipantID == id);
        }

        public async Task<List<int>> GetLosersAsync(int tournamentID)
        {
            return await this.dbContext.Participants.
                Where(p => p.TournamentID == tournamentID && p.Losses == 1).
                Select(p => p.ParticipantID).
                ToListAsync();
        }

        public async Task RemoveParticipantByIdAsync(int id)
        {

            var to_remove = await GetByIdAsync(id);
             this.dbContext.Participants.Remove(to_remove!);
            this.dbContext.SaveChangesAsync();

        }

        public async Task<Participant?> GetParticipantByUsername(string username)
        {
            return await this.dbContext.Participants.FirstOrDefaultAsync(p => p.FullName == username);
        }

        public async Task<List<int>> GetParticipantsIdsAsync(int tournamentID)
        {
             var list = await dbContext.Participants.
                Where(p => p.TournamentID == tournamentID).
                Select(p => p.ParticipantID).ToListAsync();

            return list;
                
        }

        public async Task<List<int>> GetWinnersAsync(int tournamentID)
        {
            return await this.dbContext.Participants.
                Where(p => p.TournamentID == tournamentID && p.Losses == 0).
                Select(p => p.ParticipantID).
                ToListAsync();
        }

        public Task AddWinAsync(Participant participant)
        {
            participant.Wins++;
            this.dbContext.SaveChangesAsync();
        }

        public Task AddLossAsync(Participant participant)
        {
            participant.Losses++;
            this.dbContext.SaveChangesAsync();
        }
    }
}
