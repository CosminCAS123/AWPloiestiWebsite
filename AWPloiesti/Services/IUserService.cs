using AWPloiesti.Models;

namespace AWPloiesti.Services
{
    public interface IUserService
    {
        Task<OperationResult> AddParticipantsAsync(List<Participant> participants , int tournamentID);

        Task<List<int>> GetParticipantsIdsAsync(int tournamentID);

        Task<Participant> GetByIdAsync(int id);

        Task<List<int>> GetWinnersAsync(int tournamentID);
        Task<List<int>> GetLosersAsync(int tournamentID);

        Task<Participant> GetParticipantByUsername(string username);

        Task RemoveParticipantByIdAsync(int id);

        Task AddWinAsync(Participant participant);
        Task AddLossAsync(Participant participant);


    }
}
