//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using BC.EQCS.Contracts;
//using BC.EQCS.Models;
//using BC.EQCS.Security;

//namespace BC.EQCS.Domain.Incident.ModelUpdater
//{
//    public class DefaultRiskRatingFactory : IDefaultRiskRatingFactory
//    {
//        private readonly ITreeRepository<IncidentClassModel> _incidentClassRepository;
//        private readonly Authoriser _authoriser;

//        public DefaultRiskRatingFactory( ITreeRepository<IncidentClassModel> incidentClassRepository,
//            Authoriser authoriser)
//        {
//            _incidentClassRepository = incidentClassRepository;
//            _authoriser = authoriser;
//        }

//        public IncidentModelUpdateStrategyForDefaultRiskRating Create(IModelUpdateStrategy<IncidentModel,IncidentModelUpdateStrategyKey>  updateStrategy)
//        {
//            return new IncidentModelUpdateStrategyForDefaultRiskRating(updateStrategy, _incidentClassRepository, _authoriser);
//        }
//    }
//}
