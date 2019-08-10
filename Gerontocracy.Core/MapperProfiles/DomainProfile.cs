using AutoMapper;

using en = Gerontocracy.Data.Entities;
using bo = Gerontocracy.Core.BusinessObjects;

namespace Gerontocracy.Core.MapperProfiles
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<en.Account.User, bo.Account.User>();

            CreateMap<bo.Sync.Politiker, en.Party.Politiker>();
            CreateMap<bo.Sync.Partei, en.Party.Partei>();

            CreateMap<en.Party.Partei, bo.Party.ParteiDetail>();
            CreateMap<en.Party.Partei, bo.Party.ParteiOverview>();
            CreateMap<en.Party.Politiker, bo.Party.PolitikerDetail>();
            CreateMap<en.Party.Politiker, bo.Party.PolitikerOverview>();

            CreateMap<en.Affair.Quelle, bo.Affair.QuelleOverview>();
            CreateMap<bo.Affair.Quelle, en.Affair.Quelle>();

            CreateMap<bo.Affair.Vorfall, en.Affair.Vorfall>();
        }
    }
}
