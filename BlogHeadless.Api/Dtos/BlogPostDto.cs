using AutoMapper;
using BlogHeadless.Api.Models.Ids;
using BlogHeadless.Data.Models.Author;
using BlogHeadless.Data.Models.BlogPost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogHeadless.Data.Dtos
{
    public class BlogPostDto
    {
        public string Id { get; set; }
        public string BlogHeader { get; set; }
        public string BlogBody { get; set; }
        public AuthorDto Author { get; set; }
        public string BlogPostTags { get; set; }
        public List<string> BlogPostTagsAsList { get; set; }

        // Default constructor
        public BlogPostDto() { }

        // Constructor chaining: Each constructor initializes the class further
        public BlogPostDto(string id) : this()
        {
            Id = id;
        }

        public BlogPostDto(string id, string blogHeader) : this(id)
        {
            BlogHeader = blogHeader;
        }

        public BlogPostDto(string id, string blogHeader, string blogBody) : this(id, blogHeader)
        {
            BlogBody = blogBody;
        }

        public BlogPostDto(string id, string blogHeader, string blogBody, AuthorDto author) : this(id, blogHeader, blogBody)
        {
            Author = author;
        }

        public BlogPostDto(string id, string blogHeader, string blogBody, AuthorDto author, string blogPostTags) : this(id, blogHeader, blogBody, author)
        {
            BlogPostTags = blogPostTags;
        }

        public BlogPostDto(string id, string blogHeader, string blogBody, AuthorDto author, string blogPostTags, List<string>? blogPostTagsAsList) : this(id, blogHeader, blogBody, author, blogPostTags)
        {
            BlogPostTagsAsList = blogPostTagsAsList;
        }
    }


    public record AuthorDto(string Id, string Name, string Email);

    public class BlogPostProfie:Profile
    {
        public BlogPostProfie()
        {
            CreateMap<BlogPost, BlogPostDto>()
               .ForMember(dto => dto.Id, opt => opt.MapFrom(src => src.Id.Value.ToString()))
               .ForMember(dto => dto.BlogHeader, opt => opt.MapFrom(src => src.BlogHeader.Value))
               .ForMember(dto => dto.BlogBody, opt => opt.MapFrom(src => src.BlogBody))
               .ForMember(dto => dto.Author, opt => opt.MapFrom(src => src.Author))
               .ForMember(dto => dto.BlogPostTagsAsList, opt =>opt.MapFrom(src=> src.blogPostTags !=null ?src.blogPostTags.GetTags():new List<string>())) //Can't work 

               ;


        }

    }

    public class AuthorProfile:Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(src => src.Id.Value.ToString()))
                .ForMember(dto => dto.Name, opt => opt.MapFrom(src => src.Name.Value))
                .ForMember(dto => dto.Email, opt => opt.MapFrom(src => src.Email.Value));
        }
    }
    
}
