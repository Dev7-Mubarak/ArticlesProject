using ArticlesProject.Core;

namespace ArticlesProject.Data.SqlServerEF
{
    public class AuthorEntity : IDataHelper<Author>
    {
        private DBContext _dbContext;
        private Author _Author;

        public AuthorEntity()
        {
            _dbContext = new DBContext();
        }
        public int Add(Author enrity)
        {
            if (_dbContext.Database.CanConnect())
            {
                _dbContext.Add(enrity);
                _dbContext.SaveChanges();
                return 1;
            }

            return 0;
        }
        public int Update(int Id, Author enrity)
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
                _Author = Find(Id);
                _dbContext.Remove(_Author);
                _dbContext.SaveChanges();

                return 1;
            }

            return 0;
        }

        public Author Find(int Id)
        {
            if (_dbContext.Database.CanConnect())
            {
                return _dbContext.Authors.FirstOrDefault(x => x.Id == Id);
            }

            return null;
        }

        public IEnumerable<Author> GetAll()
        {
            if (_dbContext.Database.CanConnect())
            {
                return _dbContext.Authors;
            }

            return null;
        }

        public IEnumerable<Author> GetUserById(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Author> Search(string SearchItem)
        {
            if (_dbContext.Database.CanConnect())
            {
                return _dbContext.Authors.Where(
                    x => x.FullName.Contains(SearchItem)
                    || x.UserId.ToString().Contains(SearchItem)
                    || x.Bio.Contains(SearchItem)
                    || x.Facbook.Contains(SearchItem)
                    || x.Twitter.Contains(SearchItem)
                    || x.Instragram.Contains(SearchItem)
                    );
            }

            return null;
        }
    }
}
