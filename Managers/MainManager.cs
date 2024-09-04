using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Linq.Expressions;
namespace Managers
{
    public class MainManager<T> where T : class
    {
        private readonly ProjectContext context;
        private DbSet<T> Set;

        public MainManager(ProjectContext context) { 
            this.context = context;
            this.Set = context.Set<T>();
        }

        public IQueryable<T> Filter(Expression<Func<T,bool>> expression,
            string columnName = "Id", bool IsAscending = false,
            int PageSize = 5 , int PageNumber = 1)
        {
            IQueryable<T> query = this.Set.AsQueryable();

            //Filter .........
            if (expression != null) 
                query = query.Where(expression);


            //sort .........
            if(!string.IsNullOrEmpty(columnName))
                query = query.OrderBy(columnName, IsAscending);
            

            //pagination PageSize , PageNumber
            if (PageNumber < 0) {
               PageNumber = 1;
            }
            if (PageSize < 0)
            {
               PageSize = 5;
            }
            int RowCount = query.Count();
            if (RowCount < PageSize)
            {
                PageSize = RowCount;
                PageNumber = 1;
            }
            //todo : 40 last 40
            int toSkip = (PageNumber - 1) * PageSize;
            query = query.Skip(toSkip).Take(PageSize);

            return query;
        }
        
        public IQueryable<T> GetAll() {
            return this.Set.AsQueryable();
        }
        public bool Add(T data) {
            try
            {
                this.Set.Add(data);
                this.context.SaveChanges();
                return true;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        public bool Update(T data) {
            try { 
                this.Set.Update(data);
                this.context.SaveChanges();
                return true;
            }
            catch (Exception ex) { 
                Console.WriteLine(ex.ToString());
                return false; 
            }
        }
        public bool Delete(T data) {
            try
            {
                this.Set.Remove(data);
                this.context.SaveChanges();
                return true;
            }
            catch (Exception ex) { 
                Console.WriteLine(ex.ToString());
                return false; 
            }
        }

    }
}
