using AWPloiesti.Models;

namespace AWPloiesti.Services
{
    public interface ITournamentService
    {
         Task<OperationResult> AddTournamentAsync(Tournament tournament);
    }
}
