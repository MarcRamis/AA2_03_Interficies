using System.Collections.Generic;
using UnityEngine;

namespace Code.Model.Repositories
{
    public class LocalTaskRepository : ITaskRepository
    {
        private readonly string _tasksKey = "TaskKey";

        private int _lastUsedId = -1;
        private List<TaskEntity> _tasks;

        public LocalTaskRepository()
        {
            _tasks = new List<TaskEntity>();
        }

        public IReadOnlyList<TaskEntity> GetAll()
        {
            var defaultValue = new TasksDto(new List<TaskDto>());
            var tasksJson = PlayerPrefs.GetString(_tasksKey, JsonUtility.ToJson(defaultValue));
            var tasks = JsonUtility.FromJson<TasksDto>(tasksJson);

            foreach (var taskDto in tasks.Tasks)
            {
                var taskEntity = new TaskEntity(taskDto.Id, taskDto.Text);
                _tasks.Add(taskEntity);
            }

            return _tasks;
        }


        public TaskEntity Create(string text)
        {
            _lastUsedId += 1;

            var taskEntity = new TaskEntity(_lastUsedId, text);
            _tasks.Add(taskEntity);

            SaveTasksOnPlayerPrefs();

            return taskEntity;
        }

        public void Delete(int id)
        {
            var index = _tasks.FindIndex(entity => entity.Id.Equals(id));
            _tasks.RemoveAt(index);


            // _tasks.RemoveAll(entity => entity.Id.Equals(id));

            // Guardar tasks en PlayerPrefs
            SaveTasksOnPlayerPrefs();
        }


        private void SaveTasksOnPlayerPrefs()
        {
            var taskDtos = new List<TaskDto>(_tasks.Count);
            foreach (var entity in _tasks)
            {
                taskDtos.Add(new TaskDto(entity.Id, entity.Text));
            }

            var json = JsonUtility.ToJson(new TasksDto(taskDtos));
            // Guardar tasks en PlayerPrefs
            PlayerPrefs.SetString(_tasksKey, json);
            PlayerPrefs.Save();
        }
    }
}
