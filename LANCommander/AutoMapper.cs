﻿using AutoMapper;

namespace LANCommander
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Data.Models.Action, SDK.Models.Action>();
            CreateMap<Data.Models.Archive, SDK.Models.Archive>();
            CreateMap<Data.Models.Company, SDK.Models.Company>();
            CreateMap<Data.Models.Game, SDK.Models.Game>();
            CreateMap<Data.Models.GameSave, SDK.Models.GameSave>();
            CreateMap<Data.Models.Genre, SDK.Models.Genre>();
            CreateMap<Data.Models.Key, SDK.Models.Key>();
            CreateMap<Data.Models.Media, SDK.Models.Media>();
            CreateMap<Data.Models.Redistributable, SDK.Models.Redistributable>();
            CreateMap<Data.Models.Server, SDK.Models.Server>();
            CreateMap<Data.Models.ServerConsole, SDK.Models.ServerConsole>();
            CreateMap<Data.Models.ServerHttpPath, SDK.Models.ServerHttpPath>();
            CreateMap<Data.Models.SavePath, SDK.Models.SavePath>();
            CreateMap<Data.Models.Script, SDK.Models.Script>();
            CreateMap<Data.Models.Tag, SDK.Models.Tag>();
            CreateMap<Data.Models.User, SDK.Models.User>();

            CreateMap<Models.LANCommanderSettings, SDK.Models.Settings>()
                .ForMember(dest =>
                    dest.IPXRelayHost,
                    opt => opt.MapFrom(src => src.IPXRelay.Host))
                .ForMember(dest =>
                    dest.IPXRelayPort,
                    opt => opt.MapFrom(src => src.IPXRelay.Port));
        }
    }
}
