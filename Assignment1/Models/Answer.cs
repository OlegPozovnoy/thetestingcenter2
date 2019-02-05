namespace Assignment1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Answer
    {
        public int Id { get; set; }

        public int QuestionId { get; set; }

        [Column("Answer")]
        [Required]
        public string Answer1 { get; set; }

        public double Score { get; set; }

        public virtual Question Question { get; set; }
    }
}
