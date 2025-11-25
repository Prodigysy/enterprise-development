using Microsoft.EntityFrameworkCore;
using RealEstateAgency.Domain;
using RealEstateAgency.Domain.Model;

namespace RealEstateAgency.Infrastructure.EfCore.Repository;

/// <summary>
/// Репозиторий для работы с сущностью RealEstateApplication
/// Реализует базовые операции создания, чтения, обновления и удаления
/// Использует DbContext для взаимодействия с базой данных
/// </summary>
public class RealEstateApplicationRepository(RealEstateAgencyDbContext dbContext) : IRepository<RealEstateApplication, int>
{
    /// <summary>
    /// Создает новую заявку и сохраняет её в базе данных
    /// </summary>
    /// <param name="entity">Сущность заявки для создания</param>
    public async Task<RealEstateApplication> Create(RealEstateApplication entity)
    {
        var result = await dbContext.RealEstateApplications.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return result.Entity;
    }

    /// <summary>
    /// Удаляет заявку по её идентификатору
    /// </summary>
    /// <param name="entityId">Идентификатор удаляемой заявки</param>
    public async Task<bool> Delete(int entityId)
    {
        var entity = await dbContext.RealEstateApplications.FirstOrDefaultAsync(e => e.Id == entityId);
        if (entity == null)
        {
            return false;
        }

        dbContext.RealEstateApplications.Remove(entity);
        await dbContext.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Получает заявку по идентификатору
    /// </summary>
    /// <param name="entityId">Идентификатор запрашиваемой заявки</param>
    /// <returns>Сущность заявки или null если не найдена</returns>
    public async Task<RealEstateApplication?> Get(int entityId)
    {
        return await dbContext.RealEstateApplications.FirstOrDefaultAsync(e => e.Id == entityId);
    }

    /// <summary>
    /// Получает все заявки из базы данных
    /// </summary>
    public async Task<IList<RealEstateApplication>> GetAll()
    {
        return await dbContext.RealEstateApplications.ToListAsync();
    }

    /// <summary>
    /// Обновляет существующую заявку в базе данных
    /// </summary>
    /// <param name="entity">Сущность заявки с обновленными данными</param>
    public async Task<RealEstateApplication> Update(RealEstateApplication entity)
    {
        dbContext.RealEstateApplications.Update(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }
}