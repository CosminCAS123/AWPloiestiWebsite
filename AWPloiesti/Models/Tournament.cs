using System.ComponentModel.DataAnnotations;

namespace AWPloiesti.Models
{
    public class Tournament
    {
        [Key]
        public int TournamentID { get; set; }

      
        [Required(ErrorMessage = "Camp obligatoriu!")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Doar litere!")]
        public string TournamentName { get; set; }

        public TournamentStatus Status { get; set; } = TournamentStatus.NotStarted; 

        public string CurrentStage { get; set; }

    }
}
