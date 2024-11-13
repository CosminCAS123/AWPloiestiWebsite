using AWPloiesti.Models;

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

      
    }
}
