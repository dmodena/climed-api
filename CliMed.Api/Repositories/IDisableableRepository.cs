namespace CliMed.Api.Repositories
{
    public interface IDisableableRepository<T>
    {
        void Disable(T item);
    }
}
