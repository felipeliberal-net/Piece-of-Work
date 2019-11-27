using System;
using System.ComponentModel.DataAnnotations;

namespace PoW.DataModel.Models
{
    public class TaskWork
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public TaskWorkStatus Status { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public DateTime? EditDate { get; set; }

        public DateTime? RemoveDate { get; set; }

        public DateTime? CloseDate { get; set; }
    }

    public enum TaskWorkStatus
    {
    }
}