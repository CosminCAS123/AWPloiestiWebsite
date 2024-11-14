using AWPloiesti.Models;

namespace AWPloiesti.Services
{
    public interface IUserService
    {
        Task<OperationResult> AddParticipantsAsync(List<Participant> participants , int tournamentID);

        Task<List<int>> GetParticipantsIdsAsync(int tournamentID);

        Task<Participant> GetByIdAsync(int id);


    }
}
