namespace GuardedActions.Contracts
{
    public interface IDataContext<TDataContext>
    {
        TDataContext DataContext { get; }
    }
}
