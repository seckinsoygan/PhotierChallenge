namespace Business.Abstract
{
    public interface IService<T> where T : class
    {
        Task<List<T>> GetListAsync();
        Task<List<T>> GetAsync(string query);
        Task<List<T>> DeleteAsync(int id);
        Task<string> Start(string email);
        Task<string> Complete(string file, string last_code);
    }
}
