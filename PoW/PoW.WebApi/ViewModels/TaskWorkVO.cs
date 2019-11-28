using PoW.DataModel.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace PoW.WebApi.ViewModels
{
    [DataContract]
    public sealed class TaskWorkListVO<T>
    {
        /// <summary>
        /// A list of task work
        /// </summary>
        [DataMember(Name = "taskworks"), Required]
        public List<T> TaskWorks { get; set; }
    }

    [DataContract]
    public sealed class TaskWorkWrapperVO<T>
    {
        /// <summary>
        /// A task work
        /// </summary>
        [DataMember(Name = "taskwork")]
        public T TaskWork { get; set; }
    }

    [DataContract]
    public class TaskWorkInsertVO
    {
        [DataMember(Name = "title"), Required]
        public string Title { get; set; }

        [DataMember(Name = "description"), Required]
        public string Description { get; set; }
    }

    [DataContract]
    public class TaskWorkVO
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "title"), Required]
        public string Title { get; set; }

        [DataMember(Name = "description"), Required]
        public string Description { get; set; }

        [DataMember(Name = "status"), Required]
        public TaskWorkStatus Status { get; set; }
    }
}
