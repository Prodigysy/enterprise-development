using Microsoft.EntityFrameworkCore;
using RealEstateAgency.Domain;
using RealEstateAgency.Domain.Model;

namespace RealEstateAgency.Infrastructure.EfCore.Repository;

/// <summary>
/// Репозиторий для работы с сущностью RealEstate
/// Реализует базовые операции создания, чтения, обновления и удаления
/// Использует DbContext для взаимодействия с базой данных
/// </summary>
public class RealEstateRepository(RealEstateAgencyDbContext dbContext) : IRepository<RealEstate, int>
{
    /// <summary>
    /// Создает новый объект недвижимости и сохраняет его в базе данных
    /// </summary>
    /// <param name="entity">Сущность недвижимости для создания</param>
    public async Task<RealEstate> Create(RealEstate entity)
    {
        var result = await dbContext.RealEstates.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return result.Entity;
    }

    /// <summary>
    /// Удаляет объект недвижимости по идентификатору
    /// </summary>
    /// <param name="entityId">Идентификатор удаляемого объекта</param>
    public async Task<bool> Delete(int entityId)
    {
        var entity = await dbContext.RealEstates.FirstOrDefaultAsync(e => e.Id == entityId);
        if (entity == null)
        {
            return false;
        }

        dbContext.RealEstates.Remove(entity);
        await dbContext.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Получает объект недвижимости по идентификатору
    /// </summary>
    /// <param name="entityId">Идентификатор запрашиваемого объекта</param>
    /// <returns>Сущность недвижимости или null если не найдена</returns>
    public async Task<RealEstate?> Get(int entityId)
    {
        return await dbContext.RealEstates.FirstOrDefaultAsync(e => e.Id == entityId);
    }

    /// <summary>
    /// Получает все объекты недвижимости из базы данных
    /// </summary>
    public async Task<IList<RealEstate>> GetAll()
    {
        return await dbContext.RealEstates.ToListAsync();
    }

    /// <summary>
    /// Обновляет существующий объект недвижимости в базе данных
    /// </summary>
    /// <param name="entity">Сущность недвижимости с обновленными данными</param>
    public async Task<RealEstate> Update(RealEstate entity)
    {
        dbContext.RealEstates.Update(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }
}