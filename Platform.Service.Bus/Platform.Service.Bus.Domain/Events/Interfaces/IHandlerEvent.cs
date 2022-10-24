using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Service.Bus.Domain.Events.Interfaces
{
    public interface IHandlerEvent<TEvent>  where TEvent:class
    {
        Task Execute(TEvent @event);

    }
}
