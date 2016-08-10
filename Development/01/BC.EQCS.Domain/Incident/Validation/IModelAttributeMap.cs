using System;
using System.Linq.Expressions;

namespace BC.EQCS.Domain.Incident.Validation
{
    public interface IModelAttributeMap<TModelAttributes, TTargetModel>
    {
        Expression<Func<TModelAttributes, object>> Attribute { get; }
        Expression<Func<TTargetModel, object>> Target { get; }
    }
}