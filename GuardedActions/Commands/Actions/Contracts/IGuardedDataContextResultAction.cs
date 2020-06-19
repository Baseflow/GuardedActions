using GuardedActions.Contracts;

namespace GuardedActions.Commands.Actions.Contracts
{
    public interface IGuardedDataContextResultAction<TDataContext, TResult> : IGuardedResultAction<TResult>, IDataContext<TDataContext>
    {
        void RegisterDataContext(TDataContext dataContext);
    }

    public interface IGuardedDataContextResultAction<TDataContext, TParameter, TResult> : IGuardedResultAction<TParameter, TResult>, IDataContext<TDataContext>
    {
        void RegisterDataContext(TDataContext dataContext);
    }
}
