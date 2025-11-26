using AutoMapper;
using RealEstateAgency.Application.Contracts.Counterparty;
using RealEstateAgency.Application.Contracts.RealEstate;
using RealEstateAgency.Application.Contracts.RealEstateApplication;
using RealEstateAgency.Domain.Model;

namespace RealEstateAgency.Application;

/// <summary>
/// Профиль AutoMapper для риэлторского агентства, необходимый для маппинга Entity и Dto сущностей
/// </summary>
public class RealEstateAgencyProfile : Profile
{
    /// <summary>
    /// Конструктор профиля, создающий связи между Entity и Dto сущностями
    /// </summary>
    public RealEstateAgencyProfile()
    {
        CreateMap<Counterparty, CounterpartyDto>();
        CreateMap<CounterpartyCreateUpdateDto, Counterparty>();

        CreateMap<RealEstate, RealEstateDto>();
        CreateMap<RealEstateCreateUpdateDto, RealEstate>();

        CreateMap<RealEstateApplication, RealEstateApplicationDto>();
        CreateMap<RealEstateApplicationCreateUpdateDto, RealEstateApplication>();
    }
}