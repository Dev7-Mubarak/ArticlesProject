namespace ArticlesProject.Data
{
    public interface IDataHelper<T>
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetUserById(string Id);
        IEnumerable<T> Search(string SearchItem);
        T Find(int Id);

        int Add(T enrity);
        int Update(int Id, T enrity);
        int Delete(int Id);
    }
}
