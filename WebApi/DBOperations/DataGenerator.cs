using System;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceprovider)
        {
            using (var context = new BookStoreDBContext(serviceprovider.GetRequiredService<DbContextOptions<BookStoreDBContext>>()))
            {
                // Veritabanýnda zaten veri varsa, iþlemi durdur
                if (context.Genres.Any())
                {
                    return;
                }

                // Genre verilerini ekle
                context.Genres.AddRange(
                    new Genre
                    {
                        Name = "Personal Growth",
                        Title = "Personal Growth Literature"
                    },
                    new Genre
                    {
                        Name = "Science Fiction",
                        Title = "Futuristic and Speculative Fiction"
                    },
                    new Genre
                    {
                        Name = "Romance",
                        Title = "Love and Relationships"
                    }
                );

                // Book verilerini ekle
                context.Books.AddRange(
                    new Book
                    {
                        Title = "Learn Startup",
                        GenreId = 1,
                        PageCount = 200,
                        PublisDate = new DateTime(2001, 06, 12),
                    },
                    new Book
                    {
                        Title = "Herland",
                        GenreId = 2,
                        PageCount = 250,
                        PublisDate = new DateTime(2010, 05, 23),
                    },
                    new Book
                    {
                        Title = "Dune",
                        GenreId = 1,
                        PageCount = 540,
                        PublisDate = new DateTime(2001, 12, 21),
                    }
                );

                // Deðiþiklikleri kaydet
                context.SaveChanges();
            }
        }

    }
}