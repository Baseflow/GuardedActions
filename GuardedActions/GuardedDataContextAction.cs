using GuardedActions.Interfaces;

namespace GuardedActions
{
    public abstract class GuardedDataContextAction<TDataContext> : GuardedAction, IGuardedDataContextAction<TDataContext> where TDataContext : class
    {
        public virtual void RegisterDataContext(TDataContext dataContext) => DataContext = dataContext;

        public TDataContext DataContext { get; private set; }

        public override void Dispose()
        {
            base.Dispose();
            DataContext = null;
        }
    }

    public abstract class GuardedDataContextAction<TDataContext, TParameter> : GuardedAction<TParameter>, IGuardedDataContextAction<TDataContext, TParameter> where TDataContext : class
    {
        public virtual void RegisterDataContext(TDataContext dataContext) => DataContext = dataContext;

        public TDataContext DataContext { get; private set; }

        public override void Dispose()
        {
            base.Dispose();
            DataContext = null;
        }
    }
}
