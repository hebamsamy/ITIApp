using LinqKit;
using Microsoft.Identity.Client;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Managers
{
    public class BookManager :MainManager<Book>
    {

        private ProjectContext Context;
        public BookManager(ProjectContext context) : base(context)
        {
            Context = context;
        }

        public Book GetOne(int id)
        {
            return base.GetAll().Where(b=>b.ID == id).FirstOrDefault();
        }

        public Pagination<List<BookViewModel>> Get(
            string searchText, decimal price, int SubjectId = 0, int publisherId = 0,
            string columnName = "Id", bool IsAscending = false,
            int PageSize = 5, int PageNumber = 1)
        {
            var builder = PredicateBuilder.New<Book>();

            var old = builder;

            if (!string.IsNullOrEmpty(searchText))
            {
                builder = builder.Or(b => b.Title.Contains(searchText) || b.Notes.Contains(searchText) || b.Summary.Contains(searchText));
            }
            if(price > 0)
            {
                builder = builder.Or(b=>b.Price <= price);
            }
            if (SubjectId > 0)
            {
                builder = builder.Or(b => b.SubjectId == SubjectId);
            }
            if (publisherId > 0)
            {
                builder = builder.Or(b => b.PublisherId == publisherId);
            }
            if (old == builder)
            {
                builder = null;
            }
           
            int total = (builder==null) ? 
                base.GetAll().Count() : 
                base.GetAll().Where(builder).Count();

            var quary = base.Filter(builder,columnName,IsAscending,PageSize,PageNumber);
            
            return new Pagination<List<BookViewModel>> {
                PageNumber = PageNumber,
                PageSize = PageSize,
                TotalCount = total,
                Data = quary.Select(b=> b.ToViewModel()).ToList()
            };
        }
    
        public void Update(AddBookViewModel model)
        {
            var oldData= GetOne(model.ID!.Value);

            if(model.KeepOldImages == false)
            {
                oldData.Attachments.Clear(); 
            }

            var newData = model.ToEditModel(oldData);
            base.Update(newData);

        }
    }
}
