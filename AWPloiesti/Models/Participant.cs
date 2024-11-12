using System.ComponentModel.DataAnnotations;

namespace AWPloiesti.Models
{
    public class Participant
    {
        [Key]
        public int ParticipantID { get; set; }

        [Required(ErrorMessage = "Camp obligatoriu!")]
        [RegularExpression(@"^[A-Za-z]+ [A-Za-z]+$", ErrorMessage = "Ambele nume sunt obligatorii.Doar litere.")]
        public string FullName { get; set; }

        public int TournamentID { get; set; }

        public int Wins { get; set; }

        public int Losses { get; set; }
    }
}
