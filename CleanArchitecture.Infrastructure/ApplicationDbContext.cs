using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure
{
    public sealed class ApplicationDbContext : DbContext, IUnitOfWork
    {
        private readonly IPublisher _domainEventPublisher;
        public ApplicationDbContext(DbContextOptions dbContextOptions, IPublisher domainEventPublisher) : base(dbContextOptions)
        {
            _domainEventPublisher = domainEventPublisher;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);//aplica las configuraciones de las entidades automaticamente
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await base.SaveChangesAsync(cancellationToken);

                await PublishDomainEventAsync();

                return result;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ConcurrencyException("La excepcion por concurrencia se disparó",ex);
            }
        }

        private async Task PublishDomainEventAsync() //METODO PARA PUBLICAR EVENTO
        {
            var domainEntities = ChangeTracker.Entries<Entity>()
                .Select(po => po.Entity)
                .SelectMany(entity =>
                {
                    var domainEvents = entity.GetDomainEvents();
                    entity.ClearDomainEvents();
                    return domainEvents;
                }).ToList();

            foreach (var domainEvent in domainEntities)
            {
                await _domainEventPublisher.Publish(domainEvent);
            }

        }
    }
}
