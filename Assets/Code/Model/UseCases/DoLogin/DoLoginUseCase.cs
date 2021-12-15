using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class DoLoginUseCase : IDoLoginUseCase, IDisposable
{
    private readonly IFirebaseLoginService _firebaseLoginService;
    private readonly IEventDispatcherService _eventDispatcherService;


    public DoLoginUseCase(IFirebaseLoginService firebaseLoginService, IEventDispatcherService eventDispatcherService)
    {
        _firebaseLoginService = firebaseLoginService;
        _eventDispatcherService = eventDispatcherService;
        
        _eventDispatcherService.Subscribe<LogConnectionEvent>(AlreadyConnected);
    }

    public void Login()
    {
        _firebaseLoginService.Login();
    }

    public void AlreadyConnected(LogConnectionEvent data)
    {
        if (!data.isConnected)
        {
            _firebaseLoginService.Login();
        }
    }

    public void Dispose()
    {
        _eventDispatcherService.Unsubscribe<LogConnectionEvent>(AlreadyConnected);
    }
}