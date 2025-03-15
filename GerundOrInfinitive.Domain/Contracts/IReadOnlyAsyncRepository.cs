namespace GerundOrInfinitive.Domain.Contracts;

public interface IReadOnlyAsyncRepository<T>
{
    public Task<IEnumerable<T>> GetAllItemsAsync();
}