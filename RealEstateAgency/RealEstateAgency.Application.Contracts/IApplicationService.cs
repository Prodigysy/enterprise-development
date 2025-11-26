namespace RealEstateAgency.Application.Contracts;

/// <summary>
/// Интерфейс службы приложения для CRUD операций
/// </summary>
/// <typeparam name="TDto">DTO для Get-запросов</typeparam>
/// <typeparam name="TCreateUpdateDto">DTO для Post/Put-запросов</typeparam>
/// <typeparam name="TKey">Тип идентификатора DTO</typeparam>
public interface IApplicationService<TDto, TCreateUpdateDto, TKey>
    where TDto : class
    where TCreateUpdateDto : class
    where TKey : struct
{
    /// <summary>
    /// Создание новой сущности на основе DTO
    /// </summary>
    /// <param name="dto">DTO с данными для создания</param>
    /// <returns>Созданный DTO</returns>
    public Task<TDto> Create(TCreateUpdateDto dto);

    /// <summary>
    /// Получение сущности по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор сущности</param>
    /// <returns>DTO или null если сущность не найдена</returns>
    public Task<TDto?> Get(TKey dtoId);

    /// <summary>
    /// Получение полного списка сущностей
    /// </summary>
    /// <returns>Список DTO</returns>
    public Task<IList<TDto>> GetAll();

    /// <summary>
    /// Обновление сущности по идентификатору
    /// </summary>
    /// <param name="dto">DTO с обновлёнными значениями</param>
    /// <param name="dtoId">Идентификатор обновляемой сущности</param>
    /// <returns>Обновлённый DTO</returns>
    public Task<TDto> Update(TCreateUpdateDto dto, TKey dtoId);

    /// <summary>
    /// Удаление сущности по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор удаляемой сущности</param>
    /// <returns>true если удаление выполнено успешно иначе false</returns>
    public Task<bool> Delete(TKey dtoId);
}