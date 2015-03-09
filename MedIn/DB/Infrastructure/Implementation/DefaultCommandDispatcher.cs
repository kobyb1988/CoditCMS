using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;
using DB.Exceptions;
using Libs.Services;

namespace DB.Infrastructure.Implementation
{
    public class DefaultCommandDispatcher : ICommandDispatcher
    {
		//[DebuggerStepThrough]
        public ICommandResult Submit<TCommand>(TCommand command) where TCommand: ICommand
        {
            var handler = DependencyResolver.Current.GetService<ICommandHandler<TCommand>>();
            if (handler == null)
            {
                throw new CommandHandlerNotFoundException(typeof(TCommand));
            }
			try
			{
				return handler.Execute(command);
			}
            catch(Exception exception)
            {
            	var logger = DependencyResolver.Current.GetService<ILogger>();
				logger.LogException(exception, string.Format("Ошибка выполнения комманды '{0}'", GetType().Name));
				return new CommandResult(false);
            }
        }

		[DebuggerStepThrough]
        public IEnumerable<ValidationResult> Validate<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handler = DependencyResolver.Current.GetService<IValidationHandler<TCommand>>();
            if (handler == null)
            {
                throw new ValidationHandlerNotFoundException(typeof(TCommand));
            }  
            return handler.Validate(command);
        }
    }
}

