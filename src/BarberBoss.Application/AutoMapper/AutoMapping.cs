using AutoMapper;
using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Entities;

namespace BarberBoss.Application.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            RequestToEntity();
            EntityToResponse();
        }

        private void RequestToEntity()
        {
            CreateMap<RequestRegisterServiceJson, Service>();
        }

        private void EntityToResponse()
        {
            CreateMap<Service, ResponseRegisteredServiceJson>();
            CreateMap<Service, ResponseShortServiceJson>();
            CreateMap<Service, ResponseServiceJson>();
        }
    }
}
