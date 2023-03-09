namespace Domain;

public class TeamMemberNotifier: IObservable<TeamMemberNotification>
{
    private readonly List<IObserver<TeamMemberNotification>> _observers = new();

    public void Notify(TeamMemberNotification message)
    {
        foreach (var observer in _observers)
        {
            observer.OnNext(message);
        }
    }
    public IDisposable Subscribe(IObserver<TeamMemberNotification> observer)
    {
        if (! _observers.Contains(observer)) _observers.Add(observer);
        return new Unsubscriber(_observers, observer);
    }
    
    private class Unsubscriber : IDisposable
    {
        private readonly IList<IObserver<TeamMemberNotification>> _observers;
        private readonly IObserver<TeamMemberNotification>? _observer;

        public Unsubscriber(List<IObserver<TeamMemberNotification>> observers, IObserver<TeamMemberNotification> observer)
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