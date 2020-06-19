using System.Threading.Tasks;
using GuardedActions.Contracts;

namespace GuardedActions.Commands.Actions.Contracts
{
    public interface IGuardedAction<TParameter> : IAction
    {
        Task ExecuteGuarded(TParameter parameter);
    }

    public interface IGuardedAction : IAction
    {
        Task ExecuteGuarded();
    }

    public interface IGuardedDataContextAction<TDataContext> : IGuardedAction, IDataContext<TDataContext>
        where TDataContext : class
    {
        void RegisterDataContext(TDataContext dataContext);
    }

    public interface IGuardedDataContextAction<TDataContext, TParameter> : IGuardedAction<TParameter>, IDataContext<TDataContext>
        where TDataContext : class
    {
        void RegisterDataContext(TDataContext dataContext);
    }
}
