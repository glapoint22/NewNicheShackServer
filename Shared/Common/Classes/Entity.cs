﻿using MediatR;
using Shared.Common.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Common.Classes
{
    public class Entity : IEntity
    {
        private readonly List<INotification> _domainEvents = new();

        [NotMapped]
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(INotification domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}