using Firebase.Extensions;
using Firebase.Firestore;
using Code.Model;
using UnityEngine;


public class FirebaseStoreService : IFirebaseStoreService
{
    private readonly IEventDispatcherService eventDispatcher;

    public FirebaseStoreService(IEventDispatcherService _eventDispatcher)
    {
        eventDispatcher = _eventDispatcher;
    }

    public void Save(TaskEntity taskEntity)
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        
        var currentTask = new TaskStore(taskEntity.Id, taskEntity.Text);

        DocumentReference docRef = db.Collection("users").Document(Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId).Collection("tasks").Document(currentTask.Id.ToString());
        docRef.SetAsync(currentTask.Text).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Added Data");
               //LoadData();
            }
            else
                Debug.LogError(task.Exception);
        });
    }
    public void Delete(int id)
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

        DocumentReference cityRef = db.Collection("users").Document(Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId)
            .Collection("tasks").Document(id.ToString());
        
        cityRef.DeleteAsync();
    }

    private void LoadData()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

        CollectionReference usersRef = db.Collection("users");
        usersRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot snapshot = task.Result;
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                var user = document.ConvertTo<TaskStore>();
            }
        });
    }
}