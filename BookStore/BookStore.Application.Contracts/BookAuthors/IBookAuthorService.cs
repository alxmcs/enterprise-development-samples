namespace BookStore.Application.Contracts.BookAuthors;
/// <summary>
/// Наследник аппликейшен службы для связи авторов и книг
/// </summary>
public interface IBookAuthorService : IApplicationService<BookAuthorDto, BookAuthorCreateUpdateDto, int>
{
    /// <summary>
    /// Метод для получения коллекции контрактов по шине
    /// </summary>
    /// <param name="contracts">Коллекция контрактов</param>
    Task ReceiveContractList(IList<BookAuthorCreateUpdateDto> contracts);
}
