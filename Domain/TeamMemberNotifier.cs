namespace Domain;

public class TeamMemberNotifier: IObservable<TeamMemberNotification>
{
    public List<IObserver<TeamMemberNotification>> Observers = new();
    private TeamMember _teamMember;

    public TeamMemberNotifier(TeamMember teamMember)
    {
        _teamMember = teamMember;
    }

    public void Notify(TeamMemberNotification message)
    {
        foreach (var observer in Observers)
        {
            observer.OnNext(message);
        }
    }
    public IDisposable Subscribe(IObserver<TeamMemberNotification> observer)
    {
        if (! Observers.Contains(observer)) Observers.Add(observer);
        return new Unsubscriber(Observers, observer);
    }
    
    private class Unsubscriber : IDisposable
    {
        private IList<IObserver<TeamMemberNotification>> _observers;
        private IObserver<TeamMemberNotification> _observer;

        public Unsubscriber(List<IObserver<TeamMemberNotification>> observers, IObserver<TeamMemberNotification> observer)
        {
            this._observers = observers;
            this._observer = observer;
        }

        public void Dispose()
        {
            if (_observer != null && _observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }
}