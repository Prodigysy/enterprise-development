using AutoMapper;
using RealEstateAgency.Application.Contracts;
using RealEstateAgency.Application.Contracts.Counterparty;
using RealEstateAgency.Domain;
using RealEstateAgency.Domain.Model;

namespace RealEstateAgency.Application.Service;

/// <summary>
/// Сервис для управления контрагентами;
/// Реализует CRUD операции и преобразование между сущностями и DTO
/// </summary>
public class CounterpartyService(IRepository<Counterparty, int> repository, IMapper mapper) : IApplicationService<CounterpartyDto, CounterpartyCreateUpdateDto, int>
{
    /// <summary>
    /// Создание нового контрагента
    /// </summary>
    /// <param name="dto">Данные для создания</param>
    /// <returns>Созданный контрагент</returns>
    public async Task<CounterpartyDto> Create(CounterpartyCreateUpdateDto dto)
    {
        var entity = mapper.Map<Counterparty>(dto);
        var result = await repository.Create(entity);
        return mapper.Map<CounterpartyDto>(result);
    }

    /// <summary>
    /// Удаление контрагента по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор контрагента</param>
    public async Task<bool> Delete(int dtoId)
    {
        return await repository.Delete(dtoId);
    }

    /// <summary>
    /// Получение контрагента по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор контрагента</param>
    /// <returns>Контрагент или исключение если не найден</returns>
    public async Task<CounterpartyDto?> Get(int dtoId)
    {
        var entity = await repository.Get(dtoId)
                     ?? throw new KeyNotFoundException($"Entity with Id: {dtoId} not found");
        return mapper.Map<CounterpartyDto>(entity);
    }

    /// <summary>
    /// Получение всего списка контрагентов
    /// </summary>
    /// <returns>Список контрагентов</returns>
    public async Task<IList<CounterpartyDto>> GetAll()
    {
        return mapper.Map<IList<CounterpartyDto>>(await repository.GetAll());
    }

    /// <summary>
    /// Обновление существующего контрагента
    /// </summary>
    /// <param name="dto">Новые значения</param>
    /// <param name="dtoId">Идентификатор обновляемого контрагента</param>
    /// <returns>Обновлённый контрагент</returns>
    public async Task<CounterpartyDto> Update(CounterpartyCreateUpdateDto dto, int dtoId)
    {
        var entity = await repository.Get(dtoId)
                     ?? throw new KeyNotFoundException($"Entity with Id: {dtoId} not found");

        mapper.Map(dto, entity);

        var result = await repository.Update(entity);

        return mapper.Map<CounterpartyDto>(result);
    }
}