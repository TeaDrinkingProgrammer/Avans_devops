using Domain.Notifier.Events;

namespace Domain;

public class TeamMemberNotifier: IObservable<Notification>
{
    private readonly List<IObserver<Notification>> _observers = new();

    public void Notify(Notification message)
    {
        foreach (var observer in _observers)
        {
            observer.OnNext(message);
        }
    }
    public IDisposable Subscribe(IObserver<Notification> observer)
    {
        if (! _observers.Contains(observer)) _observers.Add(observer);
        return new Unsubscriber(_observers, observer);
    }
    
    private sealed class Unsubscriber : IDisposable
    {
        private readonly IList<IObserver<Notification>> _observers;
        private readonly IObserver<Notification>? _observer;

        public Unsubscriber(List<IObserver<Notification>> observers, IObserver<Notification> observer)
        {
            _observers = observers;
            _observer = observer;
        }

        public void Dispose()
        {
            if (_observer != null && _observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }
}