using Firebase.Extensions;
using Firebase.Firestore;
using Code.Model;
using UnityEngine;
using System.Collections.Generic;
using System;
using Code.Model.UseCases.CreateTask;

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
        
        DocumentReference docRef = db
            .Collection("users").Document(Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId)
            .Collection("tasks").Document(currentTask.Id.ToString());

        docRef.SetAsync(currentTask).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Added Data");
            }
            else
                Debug.LogError(task.Exception);
        });
    }
    public void Delete(int id)
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

        DocumentReference docRef = db
            .Collection("users").Document(Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId)
            .Collection("tasks").Document(id.ToString());
        docRef.DeleteAsync();
    }

    public void LoadAll()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        
        CollectionReference docRef = db
            .Collection("users").Document(Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId)
            .Collection("tasks");

        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot snapshot = task.Result;
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                var currentText = document.ConvertTo<TextStore>();
                
                var eventData = new NewTaskCreatedEvent(int.Parse(document.Id.ToString()), currentText.Text);
                eventDispatcher.Dispatch<NewTaskCreatedEvent>(eventData);
                eventDispatcher.Dispatch<SaveTaskEvent>(new SaveTaskEvent(eventData.Text));
            }
        });

    }
}