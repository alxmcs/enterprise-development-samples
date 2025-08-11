namespace BookStore.Domain.Tets;

/// <summary>
/// ����� ������-������ �������
/// </summary>
/// <param name="fixture">�������� �������� ������</param>
public class AuthorTests(AuthorManagerFixture fixture) : IClassFixture<AuthorManagerFixture>
{
    /// <summary>
    /// ����������������� ���� ������, ������������� ��������� 5 ���� ������
    /// </summary>
    /// <param name="authorId">������������� ������</param>
    /// <param name="expectedCount"></param>
    [Theory]
    [InlineData(1, 1)]
    [InlineData(2, 1)]
    [InlineData(3, 1)]
    [InlineData(4, 2)]
    public void GetLast5AuthorsBook_Success(int authorId, int expectedCount)
    {
        var books = fixture.AuthorManager.GetLast5AuthorsBook(authorId);
        Assert.Equal(expectedCount, books.Count);
    }

    /// <summary>
    /// ����������������� ���� ������, ���������� ��� 5 ������� �� ����� �������
    /// </summary>
    [Fact]
    public void GetTop5AuthorsByPageCount_Success()
    {
        var authors = fixture.AuthorManager.GetTop5AuthorsByPageCount();
        Assert.Equal(4, authors.Count);
    }
}