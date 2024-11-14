using AWPloiesti.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Runtime.CompilerServices;

namespace AWPloiesti.Services
{
    public interface ITournamentService
    {
        int GetCurrentTournamentID();
        void SetCurrentTournamentID(int id);
         Task<OperationResult> AddTournamentAsync(Tournament tournament);

        Task<Tournament> GetTournamentByIdAsync(int id);

        //IMPLEMENT
        Task<List<(string, string)>> GetAllMatchesForStageAsync(int round);

    }
}
