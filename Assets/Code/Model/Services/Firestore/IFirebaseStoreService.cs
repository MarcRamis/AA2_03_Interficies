using Code.Model;

public interface IFirebaseStoreService
{
    TaskEntity Create(string text);
    void Delete(int id);
}
