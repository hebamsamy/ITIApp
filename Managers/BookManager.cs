﻿using LinqKit;
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
        public BookManager(ProjectContext context) : base(context)
        {
        }

        public Book GetOne(int id)
        {
            return base.GetAll().Where(b=>b.ID == id).FirstOrDefault();
        }
        public List<BookViewModel> Get(string searchText, decimal price, string columnName = "Id", bool IsAscending = false,
            int PageSize = 5, int PageNumber = 1)
        {
            var builder = PredicateBuilder.New<Book>();

            var old = builder;

            if (!string.IsNullOrEmpty(searchText))
            {
                builder = builder.Or(b => b.Title.Contains(searchText) );
            }
            if(price > 0)
            {
                builder = builder.Or(b=>b.Price <= price);
            }

            if (old == builder)
            {
                builder = null;
            }
            var quary = base.Filter(builder,columnName,IsAscending,PageSize,PageNumber);
            return quary.Select(b=> b.ToViewModel()).ToList();
        }
    }
}
