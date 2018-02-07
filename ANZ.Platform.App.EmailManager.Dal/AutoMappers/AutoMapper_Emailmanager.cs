using ANZ.Platform.App.EmailManager.Objects.BusinessModels;
using ANZ.Platform.Core.ObjectMapper;
using ANZ.Platform.Core.Objects.Database.EmailManager;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANZ.Platform.App.EmailManager.Dal.AutoMappers
{
    public class AutoMapper_Emailmanager
    {

        private EntityMapper AutoMapper;

        public AutoMapper_Emailmanager()
        {
            this.AutoMapper = new EntityMapper();
        }



        public MapperConfiguration CustomMapping_UpdateSignatureQueue()
        {
            return new MapperConfiguration(c =>
            {
                c.CreateMap<SignatureUpdateQueue, SignatureUpdateQueueBusinessModel>().ReverseMap();
                c.CreateMap<SignatureUpdateStatus, SignatureUpdateStatusBusinessModel>().ReverseMap();
                //c.CreateMap<SignatureUpdateType, SignatureUpdateTypeBusinessModel>().ReverseMap();
                c.CreateMap<Company, CompanyBusinessModel>().ReverseMap();

            });

        }



        public IEnumerable<D> MapIEnumerableSignatureUpdateObject<S, D>(IEnumerable<S> data)
        {
            var mapperConfig = CustomMapping_UpdateSignatureQueue();
            return AutoMapper.MapIEnumerableQueriedData<S, D>(mapperConfig, data);

        }


        public D MapSignatureUpdateObject<S, D>(S data)
        {
            var mapperConfig = CustomMapping_UpdateSignatureQueue();
            return AutoMapper.MapQueriedData<S, D>(mapperConfig, data);

        }

    }
}
