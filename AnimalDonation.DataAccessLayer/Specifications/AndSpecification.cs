using AnimalDonation.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalDonation.DataAccessLayer.Specifications
{
    public class AndSpecification<T> : CompositeSpecification<T>
    {
        ISpecification<T> rightSpecification;
        ISpecification<T> leftSpecification;

        public AndSpecification(ISpecification<T> right, ISpecification<T> left)
        {
            rightSpecification = right;
            leftSpecification = left;
        }

        public override bool IsSatisfiedBy(T o)
        {
            return this.leftSpecification.IsSatisfiedBy(o)
                && this.rightSpecification.IsSatisfiedBy(o);
        }
    }
}
