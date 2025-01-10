using Api.Models;

namespace Api.Services;

/// <summary>
/// Сравнитель для объектов <see cref="NewsView"/>.
/// </summary>
/// <remarks>
/// Этот класс реализует интерфейс <see cref="IComparer{T}"/> и используется для сортировки
/// объектов <see cref="NewsView"/> по дате публикации в порядке убывания.
/// </remarks>
public class NewsViewComparer : IComparer<NewsView>
{
    /// <summary>
    /// Сравнивает два объекта <see cref="NewsView"/> по дате публикации.
    /// </summary>
    /// <param name="x">Первый объект для сравнения.</param>
    /// <param name="y">Второй объект для сравнения.</param>
    /// <returns>
    /// Отрицательное число, если <paramref name="x"/> позже <paramref name="y"/>;
    /// положительное число, если <paramref name="x"/> раньше <paramref name="y"/>;
    /// ноль, если они равны.
    /// </returns>
    public int Compare(NewsView x, NewsView y)
    {
        if (x.PostDate > y.PostDate)
            return -1;

        return x.PostDate < y.PostDate ? 1 : 0;
    }
}