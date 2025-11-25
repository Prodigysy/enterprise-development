using Microsoft.EntityFrameworkCore;
using RealEstateAgency.Domain;
using RealEstateAgency.Domain.Model;

namespace RealEstateAgency.Infrastructure.EfCore.Repository;

/// <summary>
/// Репозиторий для работы с сущностью Counterparty
/// Реализует базовые операции создания, чтения, обновления и удаления
/// Использует DbContext для взаимодействия с базой данных
/// </summary>
public class CounterpartyRepository(RealEstateAgencyDbContext dbContext) : IRepository<Counterparty, int>
{
    /// <summary>
    /// Создает нового контрагента и сохраняет его в базе данных
    /// </summary>
    /// <param name="entity">Сущность контрагента для создания</param>
    public async Task<Counterparty> Create(Counterparty entity)
    {
        var result = await dbContext.Counterparties.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return result.Entity;
    }

    /// <summary>
    /// Удаляет контрагента по идентификатору
    /// </summary>
    /// <param name="entityId">Идентификатор удаляемого контрагента</param>
    public async Task<bool> Delete(int entityId)
    {
        var entity = await dbContext.Counterparties.FirstOrDefaultAsync(e => e.Id == entityId);
        if (entity == null)
        {
            return false;
        }

        dbContext.Counterparties.Remove(entity);
        await dbContext.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Получает контрагента по идентификатору
    /// </summary>
    /// <param name="entityId">Идентификатор запрашиваемого контрагента</param>
    /// <returns>Сущность контрагента или null если не найден</returns>
    public async Task<Counterparty?> Get(int entityId)
    {
        return await dbContext.Counterparties.FirstOrDefaultAsync(e => e.Id == entityId);
    }

    /// <summary>
    /// Получает всех контрагентов из базы данных
    /// </summary>
    public async Task<IList<Counterparty>> GetAll()
    {
        return await dbContext.Counterparties.ToListAsync();
    }

    /// <summary>
    /// Обновляет существующего контрагента в базе данных
    /// </summary>
    /// <param name="entity">Сущность контрагента с обновленными данными</param>
    public async Task<Counterparty> Update(Counterparty entity)
    {
        dbContext.Counterparties.Update(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }
}