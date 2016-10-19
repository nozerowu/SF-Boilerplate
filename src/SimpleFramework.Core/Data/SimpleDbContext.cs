﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleFramework.Infrastructure;
using SimpleFramework.Infrastructure.Data;
using SimpleFramework.Infrastructure.Entitys;
using SimpleFramework.Core.Entitys;
using System.Threading.Tasks;
using System.Threading;

namespace SimpleFramework.Core.Data
{
    public class SimpleDbContext : IdentityDbContext<UserEntity, RoleEntity, long, IdentityUserClaim<long>, UserRoleEntity, IdentityUserLogin<long>, IdentityRoleClaim<long>, IdentityUserToken<long>>
    {

        public SimpleDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Type> typeToRegisterCustomModelBuilders = new List<Type>();
            List<Type> typeToRegisterEntitys = new List<Type>();
            foreach (var assemblie in ExtensionManager.Assemblies)
            {
                // typeToRegisters.AddRange(assemblie.DefinedTypes.Select(t => t.AsType()));

                var entityClassTypes = assemblie.ExportedTypes.Where(x => 
                (x.GetTypeInfo().IsSubclassOf(typeof(Entity)) || x.GetTypeInfo().IsSubclassOf(typeof(EntityWithTypedId<>)) || x.GetTypeInfo().IsSubclassOf(typeof(AuditableEntity)))
                && !x.GetTypeInfo().IsAbstract && x.GetTypeInfo().IsClass);
                typeToRegisterEntitys.AddRange(entityClassTypes);

                var customModelBuilderClassTypes = assemblie.ExportedTypes.Where(x => typeof(ICustomModelBuilder).IsAssignableFrom(x) && x.GetTypeInfo().IsClass);
                typeToRegisterCustomModelBuilders.AddRange(customModelBuilderClassTypes);

            
            }


            RegisterEntities(modelBuilder, typeToRegisterEntitys);

            RegiserConvention(modelBuilder);

            base.OnModelCreating(modelBuilder);

            RegisterCustomMappings(modelBuilder, typeToRegisterCustomModelBuilders);
        }

        private static void RegiserConvention(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                if (entity.ClrType.Namespace != null)
                {
                    var nameParts = entity.ClrType.Namespace.Split('.');
                    var tableName = string.Concat(nameParts[2], "_", entity.ClrType.Name);
                    modelBuilder.Entity(entity.Name).ToTable(tableName);
                }
            }
        }

        private static void RegisterEntities(ModelBuilder modelBuilder, IEnumerable<Type> typeToRegisters)
        {
            
         //   var entityTypes = typeToRegisters.Where(x => (x.GetTypeInfo().IsSubclassOf(typeof(Entity))|| x.GetTypeInfo().IsSubclassOf(typeof(EntityWithTypedId<>))) && !x.GetTypeInfo().IsAbstract);
            foreach (var type in typeToRegisters)
            {
                modelBuilder.Entity(type);
            }
        }

        private static void RegisterCustomMappings(ModelBuilder modelBuilder, IEnumerable<Type> typeToRegisters)
        {
          //  var customModelBuilderTypes = typeToRegisters.Where(x => typeof(ICustomModelBuilder).IsAssignableFrom(x));
            foreach (var builderType in typeToRegisters)
            {
                if (builderType != null && builderType != typeof(ICustomModelBuilder))
                {
                    var builder = (ICustomModelBuilder)Activator.CreateInstance(builderType);
                    builder.Build(modelBuilder);
                }
            }
        }

        public override int SaveChanges()
        {
            // ensure auto history
            this.EnsureAutoHistory();

            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            // ensure auto history
            this.EnsureAutoHistory();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            // ensure auto history
            this.EnsureAutoHistory();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            // ensure auto history
            this.EnsureAutoHistory();

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
