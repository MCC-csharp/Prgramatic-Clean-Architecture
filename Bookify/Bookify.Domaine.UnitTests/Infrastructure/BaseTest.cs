using Bookify.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Domain.UnitTests.Infrastructure
{
    public abstract class BaseTest
    {
        public static T AssertDomaiNEventWasPublished<T>(Entity entity) where T : IDomainEvent
        {
            var domainEvent = entity.GetDomainEvents().OfType<T>().SingleOrDefault();

            return domainEvent is null ? throw new Exception($"{typeof(T).Name} was not published.") : domainEvent;
        }
    }
}
