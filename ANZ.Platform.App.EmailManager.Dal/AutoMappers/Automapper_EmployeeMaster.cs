using ANZ.Platform.App.EmailManager.Objects.BusinessModels;
using ANZ.Platform.Core.ObjectMapper;
using ANZ.Platform.Core.Objects.Database.EmployeeMaster;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANZ.Platform.App.EmailManager.Dal.AutoMappers
{
    public class Automapper_EmployeeMaster
    {

        private EntityMapper AutoMapper;

        public Automapper_EmployeeMaster()
        {
            this.AutoMapper = new EntityMapper();
        }


        public MapperConfiguration CustomMapping_EmployeeMasterListToPerson()
        {
            return new MapperConfiguration(c =>
            {
                c.CreateMap<EmployeeMasterList, PeopleBusinessModel>()
               .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(d => d.Description, opt => opt.MapFrom(src => src.PrefName));
            });

        }


        public MapperConfiguration CustomMapping_EmployeeMasterList()
        {
            return new MapperConfiguration(c =>
            {
                c.CreateMap<EmployeeMasterList, EmployeeMasterBusinessModel>().ReverseMap();
                c.CreateMap<Location, LocationBusinessModel>().ReverseMap();
                c.CreateMap<Department, DepartmentBusinessModel>().ReverseMap();
            });

        }



        public D MapEmployeeMasterList<S, D>(S data)
        {
            var mapperConfig = CustomMapping_EmployeeMasterList();
            return AutoMapper.MapQueriedData<S, D>(mapperConfig, data);

        }


        public IEnumerable<D> MapIEnumerablePeopleObject<S, D>(IEnumerable<S> data)
        {
            var mapperConfig = CustomMapping_EmployeeMasterListToPerson();
            return AutoMapper.MapIEnumerableQueriedData<S, D>(mapperConfig, data);


        }


    }
}
