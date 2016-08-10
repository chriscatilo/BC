using BC.EQCS.Entities.Models;
using BC.EQCS.Entities.Utils;
using BC.EQCS.Models;

namespace BC.EQCS.DataTransfer
{
    public static partial class Mapper
    {
        private static void MapIncidentCandidate()
        {
            AutoMapper.Mapper
                .CreateMap<IncidentCandidate, IncidentCandidateModel>()
                .ForMember(model => model.Nationality,
                    options => options.MapFrom(entity => entity.Nationality.GetValueOf(item => item.IsoCode)))
                .ReverseMap()
                .ForMember(model => model.Nationality, options => options.Ignore());

            AutoMapper.Mapper
                .CreateMap<IncidentCandidateView, IncidentCandidateViewModel>();
        }
    }
}