namespace Winx.Wasm.Domain;

/// <summary>
/// Доменная сущность феи винкс
/// </summary>
public class Fairy
{
    /// <summary>
    /// Идентификатор феи винкс
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// Отображаемое имя феи винкс
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Ссылка на изображение феи винкс
    /// </summary>
    public Uri? PhotoUrl { get; set; }
}
