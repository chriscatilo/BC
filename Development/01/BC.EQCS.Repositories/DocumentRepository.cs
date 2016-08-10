using System.Collections.Generic;
using System.Linq;
using BC.EQCS.Contracts;
using BC.EQCS.DataTransfer;
using BC.EQCS.Entities;
using BC.EQCS.Entities.Models;
using BC.EQCS.Models;

namespace BC.EQCS.Repositories
{
    public class DocumentRepository : Repository<DocumentStorage, DocumentModel>, IDocumentRepository<DocumentModel, DocumentViewModel>
    {
        public DocumentRepository(IEntityFactory entityFactory)
            : base(entityFactory)
        {
            KeyValue = action => action.Id;
        }

        public DocumentViewModel GetDocumentViewModelById(int id)
        {
            return Mapper.Map<DocumentViewModel>(GetById(id));
        }

        public void UpdateOrphenedDocuments(int ownerIdentifier, IEnumerable<DocumentViewModel> documents)
        {
            foreach (var entity in documents.ToList().Select(document => Context.Documents.First(d => d.Id == document.Id)))
            {
                entity.OwnerIdentifier = ownerIdentifier;
            }

            Context.SaveChanges();
        }

        public List<DocumentViewModel> GetDocumentViewModelsByActionId(int id)
        {
            return Context.Documents
                    .Where(p => p.OwnerType == "Action" && p.OwnerIdentifier.Value == id)
                    .ToList()
                    .Select(Mapper.Map<DocumentViewModel>)
                    .ToList();
        }

        public override void Delete(int id)
        {
            var document = Context.Documents.Find(id);

            if (document == null) return;

            Context.Documents.Remove(document);

            Context.SaveChanges();
        }
    }
}