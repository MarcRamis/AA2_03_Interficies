using System;
using System.Collections.Generic;
using UniRx;

public class DoLoginUseCase : IDoLoginUseCase, IDisposable
{
    private readonly IFirebaseLoginService firebaseLoginService;
    private readonly IEventDispatcherService eventDispatcherService;


    public DoLoginUseCase(IFirebaseLoginService _firebaseLoginService, IEventDispatcherService _eventDispatcherService)
    {
        firebaseLoginService = _firebaseLoginService;
        eventDispatcherService = _eventDispatcherService;
        eventDispatcherService.Subscribe<LogConnectionEvent>(AlreadyConnected);
    }

    public void Login()
    {
        firebaseLoginService.Login();
    }

    public void AlreadyConnected(LogConnectionEvent data)
    {
        if (data.isConnected)
        {
            eventDispatcherService.Dispatch(new LogEvent(firebaseLoginService.GetID()));
        }
    }

    public void Dispose()
    {
        eventDispatcherService.Unsubscribe<LogConnectionEvent>(AlreadyConnected);
    }
}