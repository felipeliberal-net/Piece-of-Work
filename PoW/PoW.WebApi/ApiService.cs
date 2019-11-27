using PoW.DataModel.Models;
using PoW.WebApi.Exceptions;
using PoW.WebApi.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace PoW.WebApi
{
    /// <summary>
    /// This class contains the methods to handle and to persist the TaskWorks.
    /// </summary>
    public class ApiService
    {
        private List<TaskWork> TaskWorks = new List<TaskWork>()
        {
            new TaskWork()
            {
                Id = 1,
                Title = "Task 1"
            },
            new TaskWork()
            {
                Id = 2,
                Title = "Task 2"
            },
            new TaskWork()
            {
                Id = 3,
                Title = "Task 3"
            }
        };

        /// <summary>
        /// This method returns a specific task work.
        /// </summary>
        /// <param name="id">Task work id.</param>
        /// <returns>A TaskWork object.</returns>
        public TaskWork Get(int id) => TaskWorks.Where(tw => tw.Id == id).SingleOrDefault();

        /// <summary>
        /// This method returns an entire list of task works.
        /// </summary>
        /// <returns>A list of TaskWorks objects.</returns>
        public List<TaskWork> List => TaskWorks.ToList();

        /// <summary>
        /// This method creates a new task work.
        /// </summary>
        /// <param name="vo">New task work object.</param>
        public int Insert(TaskWorkVO vo)
        {
            TaskWork entity = new TaskWork();
            entity.Title = vo.Title;

            return entity.Id;
        }

        /// <summary>
        /// This method edits an existing task work.
        /// </summary>
        /// <param name="id">Task work id.</param>
        /// <param name="vo">Task work object to be edited.</param>
        public void Update(int id, TaskWorkVO vo)
        {
            var entity = TaskWorks.Where(tw => tw.Id == id).SingleOrDefault();
            if (entity == null)
                throw new EntityNotFoundException();
        }

        /// <summary>
        /// This method removes an existing task work.
        /// </summary>
        /// <param name="id">Task work id.</param>
        public void Delete(int id)
        {
            var entity = TaskWorks.Where(tw => tw.Id == id).SingleOrDefault();
            if (entity == null)
                throw new EntityNotFoundException();
        }
    }
}
