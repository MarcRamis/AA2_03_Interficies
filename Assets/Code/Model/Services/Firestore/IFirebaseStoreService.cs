using Code.Model;
using System.Collections.Generic;

public interface IFirebaseStoreService
{
    void LoadAll();
    void Save(TaskEntity task);
    void Delete(int id);
}
