using Bogus;
using System.Runtime.CompilerServices;

namespace RealEstateAgency.Generator.RabbitMq.Host.Generator;

/// <summary>
/// Класс-расширение для генератора контрактов
/// </summary>
public static class RealEstateApplicationGeneratorExtensions
{
    /// <summary>
    /// Метод для облегчения генерации record-ов
    /// </summary>
    /// <typeparam name="T">Параметр типа генерируемых данных</typeparam>
    /// <param name="faker">Генератор данных</param>
    public static Faker<T> WithRecord<T>(this Faker<T> faker) where T : class =>
        faker.CustomInstantiator(factoryMethod: _ => (T)RuntimeHelpers.GetUninitializedObject(typeof(T)));

}