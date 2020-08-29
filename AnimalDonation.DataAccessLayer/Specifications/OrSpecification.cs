using AnimalDonation.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalDonation.DataAccessLayer.Specifications
{
    public class OrSpecification<T> : CompositeSpecification<T>
    {
        ISpecification<T> rightSpecification;
        ISpecification<T> leftSpecification;

        public OrSpecification(ISpecification<T> rightSpecification, ISpecification<T> leftSpecification)
        {
            this.rightSpecification = rightSpecification;
            this.leftSpecification = leftSpecification;
        }

        public override bool IsSatisfiedBy(T o)
        {
            return this.rightSpecification.IsSatisfiedBy(o)
                || this.leftSpecification.IsSatisfiedBy(o);
        }
    }
}
