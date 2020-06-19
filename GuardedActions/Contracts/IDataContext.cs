namespace GuardedActions.Contracts
{
    public interface IDataContext<out TDataContext>
    {
        TDataContext DataContext { get; }
    }
}
