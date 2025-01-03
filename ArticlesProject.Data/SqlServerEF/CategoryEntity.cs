using ArticlesProject.Core;

namespace ArticlesProject.Data.SqlServerEF
{
    public class CategoryEntity : IDataHelper<Category>
    {
        private DBContext _dbContext;
        private Category _category;

        public CategoryEntity()
        {
            _dbContext = new DBContext();
        }
        public int Add(Category enrity)
        {
            if (_dbContext.Database.CanConnect())
            {
                _dbContext.Add(enrity);
                _dbContext.SaveChanges();
                return 1;
            }

            return 0;
        }
        public int Update(int Id, Category enrity)
        {
            _dbContext = new DBContext();
            if (_dbContext.Database.CanConnect())
            {
                _dbContext.Update(enrity);
                _dbContext.SaveChanges();

                return 1;
            }

            return 0;
        }

        public int Delete(int Id)
        {

            if (_dbContext.Database.CanConnect())
            {
                _category = Find(Id);
                _dbContext.Remove(_category);
                _dbContext.SaveChanges();

                return 1;
            }

            return 0;
        }

        public Category Find(int Id)
        {
            if (_dbContext.Database.CanConnect())
            {
                return _dbContext.Categories.FirstOrDefault(x => x.Id == Id);
            }

            return null;
        }

        public IEnumerable<Category> GetAll()
        {
            if (_dbContext.Database.CanConnect())
            {
                return _dbContext.Categories;
            }

            return null;
        }

        public IEnumerable<Category> GetUserById(string Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> Search(string SearchItem)
        {
            if (_dbContext.Database.CanConnect())
            {
                return _dbContext.Categories.Where(x => x.Name.Contains(SearchItem));
            }

            return null;
        }
    }
}
