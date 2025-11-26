using AutoMapper;
using RealEstateAgency.Application.Contracts.Counterparty;
using RealEstateAgency.Application.Contracts.RealEstate;
using RealEstateAgency.Application.Contracts.RealEstateApplication;
using RealEstateAgency.Domain;
using RealEstateAgency.Domain.Model;

namespace RealEstateAgency.Application.Service;

/// <summary>
/// Сервис для управления недвижимостью;
/// Реализует CRUD операции и преобразование между сущностями и DTO
/// </summary>
public class RealEstateApplicationService(
    IRepository<RealEstateApplication, int> applicationRepository, 
    IRepository<RealEstate, int> realEstateRepository, 
    IRepository<Counterparty, int> counterpartyRepository, 
    IMapper mapper
) : IRealEstateApplicationService
{
    /// <summary>
    /// Создание нового объекта недвижимости
    /// </summary>
    /// <param name="dto">Данные для создания</param>
    /// <returns>Созданный объект недвижимости</returns>
    public async Task<RealEstateApplicationDto> Create(RealEstateApplicationCreateUpdateDto dto)
    {
        var entity = mapper.Map<RealEstateApplication>(dto);
        var result = await applicationRepository.Create(entity);
        return mapper.Map<RealEstateApplicationDto>(result);
    }

    /// <summary>
    /// Удаление объекта недвижимости по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор объекта недвижимости</param>
    public async Task<bool> Delete(int dtoId)
    {
        return await applicationRepository.Delete(dtoId);
    }

    /// <summary>
    /// Получение объекта недвижимости по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор объекта недвижимости</param>
    /// <returns>Объект недвижимости или исключение если не найден</returns>
    public async Task<RealEstateApplicationDto?> Get(int dtoId)
    {
        var entity = await applicationRepository.Get(dtoId)
                     ?? throw new KeyNotFoundException($"Entity with Id: {dtoId} not found");
        return mapper.Map<RealEstateApplicationDto>(entity);
    }

    /// <summary>
    /// Получение всего списка объектов недвижимости
    /// </summary>
    /// <returns>Список объектов недвижимости</returns>
    public async Task<IList<RealEstateApplicationDto>> GetAll()
    {
        return mapper.Map<IList<RealEstateApplicationDto>>(await applicationRepository.GetAll());
    }

    /// <summary>
    /// Получение контрагента, связанного с указанной заявкой
    /// </summary>
    /// <param name="id">Идентификатор заявки</param>
    /// <returns>DTO контрагента, связанного с заявкой</returns>
    public async Task<CounterpartyDto> GetCounterparty(int id)
    {
        var application = await applicationRepository.Get(id) 
            ?? throw new KeyNotFoundException($"Application with Id: {id} not found");

        var counterparty = await counterpartyRepository.Get(application.CounterpartyId) 
            ?? throw new KeyNotFoundException($"Counterparty with Id: {application.CounterpartyId} not found");

        return mapper.Map<CounterpartyDto>(counterparty);
    }

    /// <summary>
    /// Получение объекта недвижимости, связанного с указанной заявкой
    /// </summary>
    /// <param name="id">Идентификатор заявки</param>
    /// <returns>DTO объекта недвижимости, связанного с заявкой</returns>
    public async Task<RealEstateDto> GetRealEstate(int id)
    {
        var application = await applicationRepository.Get(id)
            ?? throw new KeyNotFoundException($"Application with Id: {id} not found");

        var realEstate = await realEstateRepository.Get(application.RealEstateId)
            ?? throw new KeyNotFoundException($"Real Estate with Id: {application.RealEstateId} not found");

        return mapper.Map<RealEstateDto>(realEstate);
    }

    /// <summary>
    /// Обновление существующего объекта недвижимости
    /// </summary>
    /// <param name="dto">Новые значения</param>
    /// <param name="dtoId">Идентификатор обновляемого объекта недвижимости</param>
    /// <returns>Обновлённый объект недвижимости</returns>
    public async Task<RealEstateApplicationDto> Update(RealEstateApplicationCreateUpdateDto dto, int dtoId)
    {
        var entity = await applicationRepository.Get(dtoId)
                     ?? throw new KeyNotFoundException($"Entity with Id: {dtoId} not found");

        mapper.Map(dto, entity);

        var result = await applicationRepository.Update(entity);

        return mapper.Map<RealEstateApplicationDto>(result);
    }
}