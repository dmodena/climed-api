namespace CliMed.Api.Repositories
{
    public interface IDeletableRepository<T>
    {
        void Delete(T item);
    }
}
