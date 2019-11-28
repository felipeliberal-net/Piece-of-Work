using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

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

    [Serializable]
    public enum TaskWorkStatus
    {
        /// <summary>
        /// The state is unknow
        /// </summary>
        UNKNOW = 0,

        /// <summary>
        /// ToDo
        /// </summary>
        TODO = 1,

        /// <summary>
        /// Doing
        /// </summary>
        DOING = 2,

        /// <summary>
        /// Done
        /// </summary>
        DONE = 3,

        /// <summary>
        /// Removed
        /// </summary>
        REMOVED = 4
    }
}