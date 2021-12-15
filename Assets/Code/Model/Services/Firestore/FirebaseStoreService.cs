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

    public void Save(TaskEntity task)
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        
        var currentTask = new TaskStore(task.Id, task.Text);

        DocumentReference docRef = db.Collection("tasks").Document(Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId);

        docRef.SetAsync(currentTask).ContinueWithOnMainThread(task =>
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

    }

    private void LoadData()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

        CollectionReference usersRef = db.Collection("tasks");
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