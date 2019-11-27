using PoW.DataModel.Models;
using PoW.WebApi.Exceptions;
using PoW.WebApi.Swagger;
using PoW.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace PoW.WebApi.Controllers
{
    /// <summary>
    /// Controller that contains the web api endpoints.
    /// </summary>
    public class TaskWorkController : ApiController
    {
        private readonly ApiService service = new ApiService();

        /// <summary>
        /// This method returns a specific task work.
        /// </summary>
        /// <param name="id">Task work id.</param>
        /// <returns>A TaskWork object.</returns>
        [HttpGet, Route(@"taskwork/{id:int}")]
        [ApiAvailable(Groups = new[] { "TaskWorks" })]
        [ApiResponseOk(Type = typeof(TaskWork))]
        public TaskWork Get(int id)
        {
            try
            {
                return service.Get(id);
            }
            catch (EntityNotFoundException)
            {
                Console.WriteLine("Task not found");
                return null;
            }

        }

        /// <summary>
        /// This method returns an entire list of task works.
        /// </summary>
        /// <returns>A list of TaskWorks objects.</returns>
        [HttpGet, Route(@"taskwork")]
        [ApiAvailable(Groups = new[] { "TaskWorks" })]
        [ApiResponseOk(Type = typeof(List<TaskWork>))]
        public IEnumerable<TaskWork> Get()
        {
            return service.List;
        }


        /// <summary>
        /// This method creates a new task work.
        /// </summary>
        /// <param name="vo">New task work object.</param>
        [HttpPost, Route(@"taskwork")]
        [ApiAvailable(Groups = new[] { "TaskWorks" })]
        [ApiResponseOk(Type = typeof(void))]
        public void Post([FromBody]TaskWorkVO vo)
        {
            try
            {
                service.Insert(vo);
            }
            catch (EntityNotFoundException)
            {
                Console.WriteLine("Task not found");
            }

        }


        /// <summary>
        /// This method edits an existing task work.
        /// </summary>
        /// <param name="id">Task work id.</param>
        /// <param name="vo">Task work object to be edited.</param>
        [HttpPut, Route(@"taskwork/{id:int}")]
        [ApiAvailable(Groups = new[] { "TaskWorks" })]
        [ApiResponseOk(Type = typeof(void))]
        public void Put(int id, [FromBody]TaskWorkVO vo)
        {
            try
            {
                service.Update(id, vo);
            }
            catch (EntityNotFoundException)
            {
                Console.WriteLine("Task not found");
            }

        }

        /// <summary>
        /// This method removes an existing task work.
        /// </summary>
        /// <param name="id">Task work id.</param>
        [HttpDelete, Route(@"taskwork/{id:int}")]
        [ApiAvailable(Groups = new[] { "TaskWorks" })]
        [ApiResponseOk(Type = typeof(void))]
        public void Delete(int id)
        {
            try
            {
                service.Delete(id);
            }
            catch (EntityNotFoundException)
            {
                Console.WriteLine("Task not found");
            }

        }
    }
}
