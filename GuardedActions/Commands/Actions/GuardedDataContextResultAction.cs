using System.Threading.Tasks;
using GuardedActions.Commands.Actions.Contracts;

namespace GuardedActions.Commands.Actions
{
    public abstract class GuardedDataContextResultAction<TDataContext, TResult> : IGuardedDataContextResultAction<TDataContext, TResult> where TDataContext : class
    {
        protected GuardedDataContextResultAction()
        {
        }

        public void RegisterDataContext(TDataContext dataContext) => DataContext = dataContext;

        public TDataContext DataContext { get; private set; }

        public abstract Task<TResult> ExecuteGuarded();

        public virtual void Dispose()
        {
            DataContext = null;
        }
    }

    public abstract class GuardedDataContextResultAction<TDataContext, TParameter, TResult>
        : GuardedResultAction<TParameter, TResult>,
          IGuardedDataContextResultAction<TDataContext, TParameter, TResult> where TDataContext : class
    {
        protected GuardedDataContextResultAction()
        {
        }

        public void RegisterDataContext(TDataContext dataContext) => DataContext = dataContext;

        public TDataContext DataContext { get; private set; }

        public override void Dispose()
        {
            base.Dispose();
            DataContext = null;
        }
    }
}
