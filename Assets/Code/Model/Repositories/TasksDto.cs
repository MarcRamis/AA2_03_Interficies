using System;
using System.Collections.Generic;

namespace Code.Model.Repositories
{
    [Serializable]
    public class TasksDto
    {
        public List<TaskDto> Tasks;

        public TasksDto(List<TaskDto> tasks)
        {
            Tasks = tasks;
        }
    }
}
