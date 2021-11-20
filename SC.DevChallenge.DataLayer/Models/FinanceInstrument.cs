using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SC.DevChallenge.DataLayer.Tables
{
    [Table("FinanceInstrument")]
    public class FinanceInstrument
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [StringLength(50)]
        public string Portfolio { get; set; }
        [StringLength(50)]
        public string Owner { get; set; }
        [StringLength(50)]
        public string Instrument { get; set; }
        public DateTime Date { get; set; }
        public int TimeSlot { get; set; }
    }
}
