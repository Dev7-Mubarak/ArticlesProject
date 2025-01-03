using ArticlesProject.Core;

namespace ArticlesProject.Data.SqlServerEF
{
    public class AuthorPostEntity : IDataHelper<AuthorPost>
    {
        private DBContext _dbContext;
        private AuthorPost _AuthorPost;

        public AuthorPostEntity()
        {
            _dbContext = new DBContext();
        }
        public int Add(AuthorPost enrity)
        {
            if (_dbContext.Database.CanConnect())
            {
                _dbContext.Add(enrity);
                _dbContext.SaveChanges();
                return 1;
            }

            return 0;
        }
        public int Update(int Id, AuthorPost enrity)
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
                _AuthorPost = Find(Id);
                _dbContext.Remove(_AuthorPost);
                _dbContext.SaveChanges();

                return 1;
            }

            return 0;
        }

        public AuthorPost Find(int Id)
        {
            if (_dbContext.Database.CanConnect())
            {
                return _dbContext.AuthorPosts.FirstOrDefault(x => x.Id == Id);
            }

            return null;
        }

        public IEnumerable<AuthorPost> GetAll()
        {
            if (_dbContext.Database.CanConnect())
            {
                return _dbContext.AuthorPosts;
            }

            return null;
        }

        public IEnumerable<AuthorPost> GetUserById(string Id)
        {
            if (_dbContext.Database.CanConnect())
            {
                return _dbContext.AuthorPosts.Where(x => x.UserId == Id);
            }

            return null;
        }

        public IEnumerable<AuthorPost> Search(string SearchItem)
        {
            if (_dbContext.Database.CanConnect())
            {
                return _dbContext.AuthorPosts.Where(
                    x => x.FullName.Contains(SearchItem)
                    || x.UserId.ToString().Contains(SearchItem)
                    || x.UserName.Contains(SearchItem)
                    || x.PostTitle.Contains(SearchItem)
                    || x.PostTitle.Contains(SearchItem)
                    || x.PostCategory.Contains(SearchItem)
                    || x.AuthorId.ToString().Contains(SearchItem)
                    || x.AddedDate.ToString().Contains(SearchItem)
                    || x.Id.ToString().Contains(SearchItem)
                    || x.CategoryId.ToString().Contains(SearchItem)
                    );
            }

            return null;
        }
    }
}
