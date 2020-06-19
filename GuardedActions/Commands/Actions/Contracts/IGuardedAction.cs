using System.Threading.Tasks;

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
}
