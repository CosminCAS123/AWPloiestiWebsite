using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AWPloiesti.Models
{
    public class Tournament
    {
        [Key]
        public int TournamentID { get; set; }


        [Required(ErrorMessage = "Camp obligatoriu!")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Doar litere!")]
        public string TournamentName { get; set; } = string.Empty;

        public TournamentStatus Status { get; set; } = TournamentStatus.NotStarted; 

        public int CurrentStage { get; set; }

        public string CurrentPlayer1 { get; set; } = string.Empty;
        public string CurrentPlayer2 { get; set; } = string.Empty;

       
       

    }
}
