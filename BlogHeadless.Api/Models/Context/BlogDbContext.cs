using BlogHeadless.Api.Models.Ids;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogHeadless.Api.Models.Context
{
    public class BlogDbContext: DbContext
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
                value => new BlogBody(value)
                );

            modelBuilder.Entity<BlogPost>()
                .Property(x => x.BlogHeader)
                .HasConversion(blogHeader => blogHeader.Value, 
                value => new BlogHeader(value));

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

        }



        public DbSet<BlogPost> BlogPosts { get; set; }

        public DbSet<Author> Authors { get; set; }

    }
}
