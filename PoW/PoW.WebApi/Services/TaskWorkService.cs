using PoW.DataModel;
using PoW.DataModel.Models;
using PoW.WebApi.Exceptions;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PoW.WebApi.Services
{
    /// <summary>
    /// This class contains the methods to handle and to persist the TaskWorks.
    /// </summary>
    public class TaskWorkService
    {
        /// <summary>
        /// This method returns a specific task work.
        /// </summary>
        /// <param name="id">Task work id.</param>
        /// <returns>A TaskWork object.</returns>
        public async Task<TaskWorkWrapperVO<TaskWorkVO>> GetAsync(int id)
        {
            using (var db = new PoWDbContext())
            {
                var result = new TaskWorkWrapperVO<TaskWorkVO>();
                var query = from tw in db.TaskWorks
                            where tw.Id == id
                            select new TaskWorkVO()
                            {
                                Id = tw.Id,
                                Title = tw.Title,
                                Description = tw.Description,
                                Status = tw.Status
                            };

                result.TaskWork = await query.SingleOrDefaultAsync();
                if (result.TaskWork == null)
                    throw new EntityNotFoundException();

                return result;
            }
        }

        /// <summary>
        /// This method returns an entire list of task works.
        /// </summary>
        /// <returns>A list of TaskWorks objects.</returns>
        public async Task<TaskWorkListVO<TaskWorkVO>> GetListAsync()
        {
            using (var db = new PoWDbContext())
            {
                var result = new TaskWorkListVO<TaskWorkVO>();
                var query = from tw in db.TaskWorks
                            where tw.Status != TaskWorkStatus.REMOVED
                            select new TaskWorkVO()
                            {
                                Id = tw.Id,
                                Title = tw.Title,
                                Description = tw.Description,
                                Status = tw.Status
                            };

                result.TaskWorks = await query.ToListAsync();
                return result;
            }
        }

        /// <summary>
        /// This method creates a new task work.
        /// </summary>
        /// <param name="vo">New task work object.</param>
        public async Task<int> InsertAsync(TaskWork vo)
        {
            using (var db = new PoWDbContext())
            {
                TaskWork entity = new TaskWork();
                entity.Title = vo.Title;
                entity.Description = vo.Description;
                entity.CreateDate = DateTime.Now;
                entity.Status = TaskWorkStatus.TODO;

                db.TaskWorks.Add(entity);
                await db.SaveChangesAsync();

                return entity.Id;
            }
        }

        /// <summary>
        /// This method edits an existing task work.
        /// </summary>
        /// <param name="id">Task work id.</param>
        /// <param name="vo">Task work object to be edited.</param>
        public async Task UpdateAsync(int id, TaskWork vo)
        {
            using (var db = new PoWDbContext())
            {
                var entity = await db.TaskWorks.Where(tw => tw.Id == id).SingleOrDefaultAsync();
                if (entity == null)
                    throw new EntityNotFoundException();

                switch (vo.Status)
                {
                    case TaskWorkStatus.UNKNOW:
                        throw new System.Exception("Status da tarefa inválido.");
                    case TaskWorkStatus.TODO:
                    case TaskWorkStatus.DOING:
                        entity.EditDate = DateTime.Now;
                        break;
                    case TaskWorkStatus.DONE:
                        entity.EditDate = DateTime.Now;
                        entity.CloseDate = DateTime.Now;
                        break;
                    case TaskWorkStatus.REMOVED:
                        entity.EditDate = DateTime.Now;
                        entity.RemoveDate = DateTime.Now;
                        break;
                    default:
                        break;
                }

                entity.Title = vo.Title;
                entity.Description = vo.Description;
                entity.Status = vo.Status;

                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// This method removes an existing task work.
        /// </summary>
        /// <param name="id">Task work id.</param>
        public async Task DeleteAsync(int id)
        {
            using (var db = new PoWDbContext())
            {
                var entity = await db.TaskWorks.Where(tw => tw.Id == id).SingleOrDefaultAsync();
                if (entity == null)
                    throw new EntityNotFoundException();

                entity.EditDate = DateTime.Now;
                entity.RemoveDate = DateTime.Now;
                entity.Status = TaskWorkStatus.REMOVED;

                await db.SaveChangesAsync();
            }
        }
    }
}
