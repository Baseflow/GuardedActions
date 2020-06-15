using System;
using System.Threading.Tasks;
using GuardedActions.Commands.Actions.Contracts;

namespace GuardedActions.Commands.Actions
{
    public abstract class GuardedDataContextAction<TDataContext> : GuardedAction, IGuardedDataContextAction<TDataContext> where TDataContext : class
    {
        public virtual void RegisterDataContext(TDataContext dataContext) => DataContext = dataContext;

        public TDataContext DataContext { get; private set; }

        public new Task ExecuteGuarded()
        {
            if (DataContext == null)
                throw new InvalidOperationException($"{GetType().FullName}.{nameof(ExecuteGuarded)}: {nameof(DataContext)} cannot be null make sure to register the {nameof(DataContext)} by calling the {nameof(RegisterDataContext)} method before you call the {nameof(ExecuteGuarded)} method. Maybe look if you need to override the RegisterDataContext on your command builder class and pass the data source trough to this instance.");

            return base.ExecuteGuarded();
        }

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

        public new Task ExecuteGuarded(TParameter parameter)
        {
            if (DataContext == null)
                throw new InvalidOperationException($"{GetType().FullName}.{nameof(ExecuteGuarded)}({nameof(TParameter)}): {nameof(DataContext)} cannot be null make sure to register the {nameof(DataContext)} by calling the {nameof(RegisterDataContext)} method before you call the {nameof(ExecuteGuarded)} method. Maybe look if you need to override the RegisterDataContext on your command builder class and pass the data source trough to this instance.");

            return base.ExecuteGuarded(parameter);
        }

        public override void Dispose()
        {
            base.Dispose();
            DataContext = null;
        }
    }
}
