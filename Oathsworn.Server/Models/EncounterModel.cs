using System;

namespace Oathsworn.Models
{
    public class EncounterModel
    {
        public int EncounterId { get; set; }
        public int EncounterNumber { get; set; }
        public string FreeCompanyName { get; set; }
        public DateTime DateStarted { get; set; }
    }
}