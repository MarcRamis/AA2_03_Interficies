using System.Collections.Generic;

namespace Code.Model.Repositories
{
    public interface ITaskRepository
    {
        IReadOnlyList<TaskEntity> GetAll();
        TaskEntity Create(string text);
        void Delete(int id);
    }
}