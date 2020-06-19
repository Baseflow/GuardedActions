namespace GuardedActions.Commands.Actions.Contracts
{
    public interface IGuardedDataContextAction<TDataContext> : IGuardedAction
    {
        void RegisterDataContext(TDataContext dataContext);

        TDataContext DataContext { get; }
    }

    public interface IGuardedDataContextAction<TDataContext, TParameter> : IGuardedAction<TParameter>
    {
        void RegisterDataContext(TDataContext dataContext);

        TDataContext DataContext { get; }
    }
}
