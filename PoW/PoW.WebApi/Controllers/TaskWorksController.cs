using PoW.DataModel.Models;
using PoW.WebApi.Exceptions;
using PoW.WebApi.Services;
using PoW.WebApi.Swagger;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PoW.WebApi.Controllers
{
    /// <summary>
    /// This Controller provides to the API client, methods to be consummed.
    /// </summary>
    public class TaskWorkController : ApiController
    {
        private readonly TaskWorkService service = new TaskWorkService();

        /// <summary>
        /// This method returns a specific task work.
        /// </summary>
        /// <param name="id">Task work id.</param>
        /// <returns>A TaskWork object.</returns>
        [HttpGet, Route(@"taskwork/{id:int}")]
        [ApiAvailable(Groups = new[] { "TaskWorks" })]
        [ApiResponseOk(Type = typeof(TaskWorkWrapperVO<TaskWorkVO>))]
        public async Task<HttpResponseMessage> Get(int id)
        {
            try
            {
                var taskwork = await service.GetAsync(id);
                return Request.CreateResponse(HttpStatusCode.OK, taskwork);
            }
            catch (EntityNotFoundException)
            {
                Console.WriteLine("Task not found");
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Tarefa não encontrada.");
            }

        }

        /// <summary>
        /// This method returns an entire list of task works.
        /// </summary>
        /// <returns>A list of TaskWorks objects.</returns>
        [HttpGet, Route(@"taskwork")]
        [ApiAvailable(Groups = new[] { "TaskWorks" })]
        [ApiResponseOk(Type = typeof(TaskWorkListVO<TaskWorkVO>))]
        public async Task<HttpResponseMessage> Get()
        {
            try
            {
                var list = await service.GetListAsync();
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch (EntityNotFoundException)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Tarefa não encontrada.");
            }
        }

        /// <summary>
        /// This method creates a new task work.
        /// </summary>
        /// <param name="vo">New task work object.</param>
        [HttpPost, Route(@"taskwork")]
        [ApiAvailable(Groups = new[] { "TaskWorks" })]
        [ApiResponseOk(Type = typeof(int))]
        public async Task<HttpResponseMessage> Post([FromBody]TaskWork vo)
        {
            try
            {
                var id = await service.InsertAsync(vo);
                return Request.CreateResponse(HttpStatusCode.OK, id);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Ocorreu um erro ao salvar a tarefa.");
            }
        }


        /// <summary>
        /// This method edits an existing task work.
        /// </summary>
        /// <param name="id">Task work id.</param>
        /// <param name="vo">Task work object to be edited.</param>
        [HttpPut, Route(@"taskwork")]
        [ApiAvailable(Groups = new[] { "TaskWorks" })]
        [ApiResponseOk]
        public async Task<HttpResponseMessage> Put(int id, [FromBody]TaskWork vo)
        {
            try
            {
                await service.UpdateAsync(id, vo);
                return Request.CreateResponse(HttpStatusCode.OK, "Tarefa atualizada com sucesso.");
            }
            catch (EntityNotFoundException)
            {
                Console.WriteLine("Task not found");
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Ocorreu um erro ao salvar a tarefa.");
            }

        }

        /// <summary>
        /// This method removes an existing task work.
        /// </summary>
        /// <param name="id">Task work id.</param>
        [HttpDelete, Route(@"taskwork")]
        [ApiAvailable(Groups = new[] { "TaskWorks" })]
        [ApiResponseOk]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                await service.DeleteAsync(id);
                return Request.CreateResponse(HttpStatusCode.OK, "Tarefa removida com sucesso.");
            }
            catch (EntityNotFoundException)
            {
                Console.WriteLine("Task not found");
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Ocorreu um erro ao remover a tarefa.");
            }

        }
    }
}
