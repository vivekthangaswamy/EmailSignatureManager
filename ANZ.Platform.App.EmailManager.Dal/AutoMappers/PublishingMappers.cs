using ANZ.Platform.App.EmailManager.Objects.BusinessModels;
using ANZ.Platform.Core.ObjectMapper;
using ANZ.Platform.Core.Objects.Database.EmailManager;
using ANZ.Platform.Core.Objects.Database.EmployeeMaster;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANZ.Platform.App.EmailManager.Dal.AutoMappers
{
    public class PublishingMappers
    {

        private EntityMapper AutoMapper;

        public PublishingMappers()
        {
            this.AutoMapper = new EntityMapper();
        }


        //----------------------------------------------------
        // Mapping Configuration
        //----------------------------------------------------


        public MapperConfiguration PublishMapper_EmailSignatureBanner()
        {
            return new MapperConfiguration(c =>
            {
                c.CreateMap<SignatureUpdateQueueBusinessModel, EmployeeMasterList>()
               .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(d => d.MarketingImagePath, opt => opt.MapFrom(src => src.MarketingImageBanner))
               .ForMember(d => d.MarketingUrl, opt => opt.MapFrom(src => src.MarketingImageUrl));
            });

        }


        //----------------------------------------------------
        // Returned Object List Mapper | Uses: Desired Mapping Configuration
        //----------------------------------------------------

        public D MapBannerObject<S, D>(S data)
        {
            var mapperConfig = PublishMapper_EmailSignatureBanner();
            return AutoMapper.MapQueriedData<S, D>(mapperConfig, data);


        }


        public D PublishingMapperObjects<S, D>(S data)
        {
            var mapperConfig = PublishMapper_EmailSignatureBanner();
            return AutoMapper.MapQueriedData<S, D>(mapperConfig, data);

        }


    }
}
