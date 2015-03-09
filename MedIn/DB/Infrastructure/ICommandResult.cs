namespace DB.Infrastructure
{
    public interface ICommandResult
    {
    	object Result { get; }
        bool Success { get; }
    }
}

