using GuardedActions.Contracts;

namespace GuardedActions.Commands.Contracts
{
    public interface IAsyncGuardedDataContextCommandBuilder<TDataContext, TCommandParameter> : IAsyncGuardedCommandBuilder<TCommandParameter>, IDataContext<TDataContext>
    {
        IAsyncGuardedDataContextCommandBuilder<TDataContext, TCommandParameter> RegisterDataContext(TDataContext dataContext);
    }

    public interface IAsyncGuardedDataContextCommandBuilder<TDataContext> : IAsyncGuardedCommandBuilder, IDataContext<TDataContext>
    {
        IAsyncGuardedDataContextCommandBuilder<TDataContext> RegisterDataContext(TDataContext dataContext);
    }
}
