using System.Threading.Tasks;

namespace GuardedActions.Interfaces
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
