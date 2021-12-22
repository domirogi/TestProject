using AutoMapper;
using Project.MVC.ViewModels;
using Project.Service.Models;
using Project.Service.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Project.MVC.AutoMapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<VehicleMake, VehicleMakeViewModel>();
            CreateMap<VehicleMakeViewModel, VehicleMake>();
            CreateMap<VehicleModel, VehicleModelViewModel>()
                .ForMember(m => m.Make, opt => opt.MapFrom
                (src => src.Make.Name));

            CreateMap<VehicleMake, VehicleMakeDropDown>();

            CreateMap<VehicleModelViewModel, VehicleModel>();

            CreateMap<IPagedList<VehicleMake>, IPagedList<VehicleMakeViewModel>>()
                .ConvertUsing(new PagedListConverter<VehicleMake, VehicleMakeViewModel>());


            CreateMap<VehicleModelViewModel, VehicleModel>();
            CreateMap<IPagedList<VehicleModel>, IPagedList<VehicleModelViewModel>>()
                .ConvertUsing(new PagedListConverter<VehicleModel, VehicleModelViewModel>());



            CreateMap<ModelSorting, IModelSorting>();
            CreateMap<FilterModel, IFilterModel>();
            CreateMap<ModelPaging, IModelPaging>();

        }
    }
}
