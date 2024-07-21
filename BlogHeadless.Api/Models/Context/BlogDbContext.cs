using BlogHeadless.Api.Models.Ids;
using BlogHeadless.Data.Models.Author;
using BlogHeadless.Data.Models.BlogPost;
using BlogHeadless.Data.Models.Subscriber;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogHeadless.Api.Models.Context
{
    public class BlogDbContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=BlogHeadless;Integrated Security=True;TrustServerCertificate=True");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogPost>()
            .Property(x => x.Id)
            .HasConversion(
            id => id.Value,  // DB'ye yazarken
            value => new BlogPostId(value)  // DB'den okurken
        );

            modelBuilder.Entity<BlogPost>()
                .Property(x => x.BlogBody)
                .HasConversion(blogbody => blogbody.Value,
                value => new BlogPostBody(value)
                );

            modelBuilder.Entity<BlogPost>()
                .Property(x => x.BlogHeader)
                .HasConversion(blogHeader => blogHeader.Value,
                value => new BlogPostHeader(value));

            modelBuilder.Entity<BlogPost>()
             .Property(x => x.blogPostTags)
             .HasConversion(blogHeader => blogHeader.Value,
             value => new BlogPostTags(value));

            modelBuilder.Entity<Author>()
                .Property(x => x.Id)
                .HasConversion(id => id.Value,
                value => new AuthorId(value)
                );

            modelBuilder.Entity<Author>()
                .Property(x => x.Email)
                .HasConversion(id => id.Value,
                value => new Email(value)
                );

            modelBuilder.Entity<Author>()
               .Property(x => x.Name)
               .HasConversion(name => name.Value,
               value => new AuthorName(value)
               );

            modelBuilder.Entity<Subscriber>()
                .Property(x => x.Id)
                .HasConversion(new UlidStringConverter())
                .HasMaxLength(26);

            modelBuilder.Entity<Subscriber>()
               .Property(x => x.Email)
               .HasConversion(email => email.Value,
               value => new Email(value)
               );

        }



        public DbSet<BlogPost> BlogPosts { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Subscriber> subscribers { get; set; }


    }

    public class UlidStringConverter : ValueConverter<Ulid, string>
    {
        public UlidStringConverter()
            : base(
               ulid => ulid.ToString(),
               asString => Ulid.Parse(asString)
               )
        { }

    }


}