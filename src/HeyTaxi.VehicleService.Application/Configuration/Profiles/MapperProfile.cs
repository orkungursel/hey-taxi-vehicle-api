using AutoMapper;
using HashidsNet;
using HeyTaxi.VehicleService.Application.Commands.AddVehicle;
using HeyTaxi.VehicleService.Application.Models.DTOs;
using HeyTaxi.VehicleService.Domain.Entities;

namespace HeyTaxi.VehicleService.Application.Configuration.Profiles;

public class MapperProfile : Profile
{
    public MapperProfile(IHashids hashids)
    {
        CreateMap<Vehicle, VehicleDTO>()
            .ForMember(m => m.Id, opt => opt.MapFrom(src => hashids.EncodeLong(src.Id)))
            .ReverseMap()
            .ForMember(m => m.Id, opt => opt.MapFrom(src => hashids.DecodeSingleLong(src.Id)));

        CreateMap<Vehicle, VehicleWithDriverDTO>()
            .ForMember(m => m.Id, opt => opt.MapFrom(src => hashids.EncodeLong(src.Id)));

        CreateMap<Driver, DriverDTO>()
            .ForMember(m => m.Id, opt => opt.MapFrom(src => src.DriverId))
            .ReverseMap();

        CreateMap<AddVehicleRequest, Vehicle>();
    }
}
