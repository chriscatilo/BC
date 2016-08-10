using System;
using BC.EQCS.Entities.Models;
using BC.EQCS.Models;

namespace BC.EQCS.DataTransfer
{
    public static partial class Mapper
    {
        private static void MapDocument()
        {
            // Model To Entity
            AutoMapper.Mapper.CreateMap<DocumentModel, DocumentStorage>().ForMember(x => x.UploadedDate, o => o.UseValue(DateTime.Now));

            // Entity To Model
            AutoMapper.Mapper.CreateMap<DocumentStorage, DocumentModel>();

            // Model To Entity
            AutoMapper.Mapper.CreateMap<DocumentViewModel, DocumentStorage>();

            // Entity To Model
            AutoMapper.Mapper.CreateMap<DocumentStorage, DocumentViewModel>();

            // Model To ViewModel
            AutoMapper.Mapper.CreateMap<DocumentViewModel, DocumentModel>();

            // ViewModel To Model
            AutoMapper.Mapper.CreateMap<DocumentModel, DocumentViewModel>();
        }
    }
}