using System;
using System.Linq.Expressions;

namespace BC.EQCS.Domain.Schema
{
    public class SchemaMemberAggregate<TAttributes, TTargetModel>
    {
        public Expression<Func<TAttributes, dynamic>> AttributesMember { get; set; }
        public Expression<Func<TTargetModel, dynamic>> TargetMember { get; set; }
        public MemberSchema SchemaMember { get; set; }

        public override string ToString()
        {
            return SchemaMember.ToString();
        }
    }
}