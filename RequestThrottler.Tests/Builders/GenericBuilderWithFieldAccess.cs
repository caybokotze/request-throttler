using System;
using PeanutButter.RandomGenerators;

namespace RequestThrottler.Tests.Builders
{
    public class GenericBuilderWithFieldAccess<TBuilder, TEntity> : GenericBuilder<TBuilder, TEntity> where TBuilder : GenericBuilder<TBuilder, TEntity>
    {
        protected TBuilder WithField(Action<TBuilder> action)
        {
            action(this as TBuilder);
            return this as TBuilder;
        }
    }
}