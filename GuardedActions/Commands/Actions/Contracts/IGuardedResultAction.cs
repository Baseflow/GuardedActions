using System.Threading.Tasks;
using GuardedActions.Contracts;

namespace GuardedActions.Commands.Actions.Contracts
{
    public interface IGuardedResultAction<TParameter, TResult> : IAction
    {
        Task<TResult> ExecuteGuarded(TParameter parameter);
    }

    public interface IGuardedResultAction<TResult> : IAction
    {
        Task<TResult> ExecuteGuarded();
    }

    public interface IGuardedDataContextResultAction<TDataContext, TResult> : IGuardedResultAction<TResult>, IDataContext<TDataContext>
        where TDataContext : class
    {
        void RegisterDataContext(TDataContext dataContext);
    }

    public interface IGuardedDataContextResultAction<TDataContext, TParameter, TResult> : IGuardedResultAction<TParameter, TResult>, IDataContext<TDataContext>
        where TDataContext : class
    {
        void RegisterDataContext(TDataContext dataContext);
    }
}
