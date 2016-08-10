using System.Collections.Generic;

namespace BC.EQCS.Contracts
{
    public interface IAspectRepository<TAspectModel, in TParentModel> : IRepository<TAspectModel>
    {
        //BRYAN: I think this implementation limits the parent class to only be able to retrieve one instance of a collection of a particular type
        //to get around this you would need to make all collection types distinct or refactor this interface to allow you to specify the member name to assign to
        //TODO: Look into this
        IEnumerable<TAspectModel> GetFor(TParentModel parentModel);
    }
}