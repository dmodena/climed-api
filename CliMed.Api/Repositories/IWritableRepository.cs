namespace CliMed.Api.Repositories
{
    public interface IWritableRepository<T>
    {
        T Create(T item);
        T Update(T item);
    }
}
