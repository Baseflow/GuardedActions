namespace GuardedActions.Interfaces
{
    public interface IGuardedDataContextResultAction<TDataContext, TResult> : IGuardedResultAction<TResult>
    {
        void RegisterDataContext(TDataContext dataContext);

        TDataContext DataContext { get; }
    }

    public interface IGuardedDataContextResultAction<TDataContext, TParameter, TResult> : IGuardedResultAction<TParameter, TResult>
    {
        void RegisterDataContext(TDataContext dataContext);

        TDataContext DataContext { get; }
    }
}
