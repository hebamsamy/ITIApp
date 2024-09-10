using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ViewModel
{
    public static class BookExtainsions
    {
        public static Book ToModel( this AddBookViewModel data)
        {
            var temp = new List<BookAttachment>();
            foreach (string item in data.ImagePaths)
            {
                temp.Add(new BookAttachment { Path = item });
            }

            return new Book
            {
                ID = data.ID == null ? 0 : data.ID.Value,
                Isbn = data.Isbn,
                Title = data.Title,
                SubjectId = data.SubjectId,
                Summary = data.Summary,
                Price = data.Price,
                PublicationDate = data.PublicationDate,
                PublisherId = data.PublisherId,
                PageCount = data.PageCount,
                Notes = data.Notes,
                Attachments = temp
            };
        }
        public static Book ToEditModel(this AddBookViewModel data, Book oldData)
        {
            var temp = new List<BookAttachment>();
            foreach (string item in data.ImagePaths)
            {
                temp.Add(new BookAttachment { Path = item });
            }

            oldData.Isbn = data.Isbn;
            oldData.Title = data.Title;
            oldData.SubjectId = data.SubjectId;
            oldData.Summary = data.Summary;
            oldData.Price = data.Price;
            oldData.PublicationDate = data.PublicationDate;
            oldData.PublisherId = data.PublisherId;
            oldData.PageCount = data.PageCount;
            oldData.Notes = data.Notes;
            oldData.Attachments = temp;
            
            return oldData;
        }
        public static BookViewModel ToViewModel(this Book book)
        {
            return new BookViewModel
            {
                ID = book.ID,
                Title = book.Title,
                SubjectId = book.SubjectId,
                SubjectName = book.Subject.Name,
                PublisherId = book.PublisherId,
                PublisherName = $"{book.Publisher.User.FirstName} {book.Publisher.User.LastName}" ,
                PublicationDate = book.PublicationDate,
                PageCount = book.PageCount,
                Isbn = book.Isbn,
                Notes = book.Notes,
                Price = book.Price,
                Summary = book.Summary,
                Attachments = book.Attachments.Select(i => i.Path).ToList(),
                Authers = book.Authers.Select(i => $"{i.Author.User.FirstName} {i.Author.User.LastName}").ToList(),
            };
        }

        public static AddBookViewModel ToAddViewModel(this Book book)
        {
            return new AddBookViewModel
            {
                ID = book.ID,
                Title = book.Title,
                SubjectId = book.SubjectId,
                PublisherId = book.PublisherId,
                PublicationDate = book.PublicationDate,
                PageCount = book.PageCount,
                Isbn = book.Isbn,
                Notes = book.Notes,
                Price = book.Price,
                Summary = book.Summary,
                ImagePaths = book.Attachments.Select(i => i.Path).ToList(),
            };
        }

    }
}
