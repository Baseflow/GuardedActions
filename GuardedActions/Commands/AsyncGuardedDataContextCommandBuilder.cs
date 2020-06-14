using System.Threading.Tasks;
using AsyncAwaitBestPractices.MVVM;
using GuardedActions.Commands.Interfaces;

namespace GuardedActions.Commands
{
    public abstract class AsyncGuardedDataContextCommandBuilder<TDataContext, TCommandParameter> : AsyncGuardedCommandBuilder<TCommandParameter>, IAsyncGuardedDataContextCommandBuilder<TDataContext, TCommandParameter> where TDataContext : class
    {
        protected TDataContext DataContext { get; private set; }

        public virtual IAsyncGuardedDataContextCommandBuilder<TDataContext, TCommandParameter> RegisterDataContext(TDataContext dataContext)
        {
            if (dataContext == null)
            {
                // TODO Add logging
            }

            DataContext = dataContext;
            return this;
        }

        protected override Task BuildCommandBody(IAsyncCommand<TCommandParameter> command, TCommandParameter model)
        {
            if (DataContext == null)
            {
                // TODO: Find framework independent solution
                //Messenger.Publish(new ExceptionMessage(this, new DataContextNotRegisteredForCommandBuilderException(GetType(), typeof(TDataContext))));
                return Task.CompletedTask;
            }

            return base.BuildCommandBody(command, model);
        }

        public override void Dispose()
        {
            base.Dispose();
            DataContext = null;
        }
    }

    public abstract class AsyncGuardedDataContextCommandBuilder<TDataContext> : AsyncGuardedCommandBuilder, IAsyncGuardedDataContextCommandBuilder<TDataContext> where TDataContext : class
    {
        protected TDataContext DataContext { get; private set; }

        public virtual IAsyncGuardedDataContextCommandBuilder<TDataContext> RegisterDataContext(TDataContext dataContext)
        {
            if (dataContext == null)
            {
                // TODO Add logging
            }

            DataContext = dataContext;
            return this;
        }

        protected override Task BuildCommandBody(IAsyncCommand command)
        {
            if (DataContext == null)
            {
                // TODO: Find framework independent solution
                //Messenger.Publish(new ExceptionMessage(this, new DataContextNotRegisteredForCommandBuilderException(GetType(), typeof(TDataContext))));
                return Task.CompletedTask;
            }

            return base.BuildCommandBody(command);
        }

        public override void Dispose()
        {
            base.Dispose();
            DataContext = null;
        }
    }
}
