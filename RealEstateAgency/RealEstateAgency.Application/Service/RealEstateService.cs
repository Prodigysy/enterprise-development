using AutoMapper;
using RealEstateAgency.Application.Contracts;
using RealEstateAgency.Application.Contracts.RealEstate;
using RealEstateAgency.Domain;
using RealEstateAgency.Domain.Model;

namespace RealEstateAgency.Application.Service;

/// <summary>
/// Сервис для управления недвижимостью;
/// Реализует CRUD операции и преобразование между сущностями и DTO
/// </summary>
public class RealEstateService(IRepository<RealEstate, int> repository, IMapper mapper) : IApplicationService<RealEstateDto, RealEstateCreateUpdateDto, int>
{
    /// <summary>
    /// Создание нового объекта недвижимости
    /// </summary>
    /// <param name="dto">Данные для создания</param>
    /// <returns>Созданный объект недвижимости</returns>
    public async Task<RealEstateDto> Create(RealEstateCreateUpdateDto dto)
    {
        var entity = mapper.Map<RealEstate>(dto);
        var result = await repository.Create(entity);
        return mapper.Map<RealEstateDto>(result);
    }

    /// <summary>
    /// Удаление объекта недвижимости по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор объекта недвижимости</param>
    public async Task<bool> Delete(int dtoId)
    {
        return await repository.Delete(dtoId);
    }

    /// <summary>
    /// Получение объекта недвижимости по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор объекта недвижимости</param>
    /// <returns>Объект недвижимости или исключение если не найден</returns>
    public async Task<RealEstateDto?> Get(int dtoId)
    {
        var entity = await repository.Get(dtoId)
                     ?? throw new KeyNotFoundException($"Entity with Id: {dtoId} not found");
        return mapper.Map<RealEstateDto>(entity);
    }

    /// <summary>
    /// Получение всего списка объектов недвижимости
    /// </summary>
    /// <returns>Список объектов недвижимости</returns>
    public async Task<IList<RealEstateDto>> GetAll()
    {
        return mapper.Map<IList<RealEstateDto>>(await repository.GetAll());
    }

    /// <summary>
    /// Обновление существующего объекта недвижимости
    /// </summary>
    /// <param name="dto">Новые значения</param>
    /// <param name="dtoId">Идентификатор обновляемого объекта недвижимости</param>
    /// <returns>Обновлённый объект недвижимости</returns>
    public async Task<RealEstateDto> Update(RealEstateCreateUpdateDto dto, int dtoId)
    {
        var entity = await repository.Get(dtoId)
                     ?? throw new KeyNotFoundException($"Entity with Id: {dtoId} not found");

        mapper.Map(dto, entity);

        var result = await repository.Update(entity);

        return mapper.Map<RealEstateDto>(result);
    }
}