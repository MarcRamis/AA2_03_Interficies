using Code.Model;

public interface IFirebaseStoreService
{
    void Save(TaskEntity task);
    void Delete(int id);
}
