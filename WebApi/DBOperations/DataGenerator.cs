using System;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceprovider)
        {
            using (var context = new BookStoreDbContext(serviceprovider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
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

                // Author verilerini ekle
                context.Authors.AddRange(
                    new Author
                    {
                        FirstName = "George",
                        LastName = "Orwell",
                        DateOfBirth = new DateTime(1903, 06, 25),
                        IsActive = true
                    },
                    new Author
                    {
                        FirstName = "Virginia",
                        LastName = "Woolf",
                        DateOfBirth = new DateTime(1882, 01, 25),
                        IsActive = true
                    },
                    new Author
                    {
                        FirstName = "Isaac",
                        LastName = "Asimov",
                        DateOfBirth = new DateTime(1920, 01, 02),
                        IsActive = true
                    }
                );


                // Deðiþiklikleri kaydet
                context.SaveChanges();
            }
        }

    }
}