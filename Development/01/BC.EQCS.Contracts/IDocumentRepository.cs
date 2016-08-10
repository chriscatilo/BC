using System.Collections.Generic;
using BC.EQCS.Models;

namespace BC.EQCS.Contracts
{
    public interface IDocumentRepository<model, viewModel> : IRepository<model>
    {
        viewModel GetDocumentViewModelById(int id);
        void UpdateOrphenedDocuments(int ownerIdentifier, IEnumerable<DocumentViewModel> model);
        List<DocumentViewModel> GetDocumentViewModelsByActionId(int id);
    }
}
