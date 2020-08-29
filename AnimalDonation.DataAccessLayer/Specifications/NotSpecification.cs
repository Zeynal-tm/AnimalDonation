using AnimalDonation.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalDonation.DataAccessLayer.Specifications
{
    public class NotSpecification<T> : CompositeSpecification<T>
    {
        ISpecification<T> specification;

        public NotSpecification(ISpecification<T> specification)
        {
            this.specification = specification;
        }

        public override bool IsSatisfiedBy(T o)
        {
            return !this.specification.IsSatisfiedBy(o);
        }
    }
}
