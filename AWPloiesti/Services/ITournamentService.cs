using AWPloiesti.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Runtime.CompilerServices;

namespace AWPloiesti.Services
{
    public interface ITournamentService
    {
       
         Task<OperationResult> AddTournamentAsync(Tournament tournament);

        Task<Tournament> GetTournamentByIdAsync(int id);

        //IMPLEMENT
        Task<List<(string, string)>> GetAllMatchesForStageAsync(int round);

        Task AddStageAsync();

        int GetCurrentStage();

        Task SetOwnTournamentAsync(int id);

        int? GetCurrentTournamentID();

    }
}
