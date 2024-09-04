using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public static class BookExtainsions
    {

        public static BookViewModel ToViewModel(this Book book)
        {
            return new BookViewModel
            {
                ID = book.ID,
                Title = book.Title,
                SubjectId = book.SubjectId,
                SubjectName = book.Subject.Name,
                PublisherId = book.PublisherId,
                PublisherName = book.Publisher.Name,
                PublicationDate = book.PublicationDate,
                PageCount = book.PageCount,
                Isbn = book.Isbn,
                Notes = book.Notes,
                Price = book.Price,
                Summary = book.Summary,
                Attachments = book.Attachments.Select(i => i.Path).ToList(),
                Authers = book.Authers.Select(i => i.Author.Name).ToList(),
            };
        }
    }
}
