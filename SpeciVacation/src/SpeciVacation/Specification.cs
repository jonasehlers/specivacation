﻿using System;
using System.Linq.Expressions;

namespace SpeciVacation
{
    /// <summary>
    /// This base specification implements the IsSatisfiedBy method by compiling the expression from ToExpression.
    ///
    /// This is useful for general specifications to prevent duplicated logic.
    /// Beware that it is not very performant in situations where many specifications are dynamically constructed and combined.   
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Specification<T> : ISpecification<T>
    {
        public static readonly ISpecification<T> All = new IdentitySpecification<T>();
        private Func<T, bool> _predicate;

        public bool IsSatisfiedBy(T entity)
        {
            _predicate = _predicate ?? ToExpression().Compile();
            return _predicate(entity);
        }

        public abstract Expression<Func<T, bool>> ToExpression();
    }
}
