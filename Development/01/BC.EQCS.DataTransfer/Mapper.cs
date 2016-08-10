using BC.EQCS.Entities.Models;
using BC.EQCS.Models;

namespace BC.EQCS.DataTransfer
{
    public static partial class Mapper
    {
        static Mapper()
        {
            SetUp();
        }

        public static TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }

        public static TDestination Map<TDestination>(object source)
        {
            return AutoMapper.Mapper.Map<TDestination>(source);
        }

        public static void SetUp()
        {
            MapReferences();
            MapIncident();
            MapIncidentActivityLog();
            MapIncidentActivityListing();
            MapIncidentActionListing();
            MapIncidentAction();
            MapIncidentActionView();
            MapUser();
            MapIncidentsListingView();
            MapApplicationAsset();
            MapAdminUnit();
            MapIncidentClass();
            MapIncidentCandidate();
            MapDocument();
            MapNotifications();
            MapNotificationMessage();
        }

        private static void MapIncidentActivityListing()
        {
            AutoMapper.Mapper.CreateMap<IncidentActivityListingView, IncidentActivityListingModel>()
                .ForMember(actlist => actlist.Username, options => options.MapFrom(source => source.Username));
        }

        private static void MapIncidentActionListing()
        {
            AutoMapper.Mapper.CreateMap<IncidentActionListing, IncidentActionListingModel>();
        }
    }
}


