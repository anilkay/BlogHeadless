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
    public  record BlogPostDto(string Id,string BlogHeader,string BlogBody);
    public record AuthorDto(string Id, string Name, string Email);

    public class BlogPostProfie:Profile
    {
        public BlogPostProfie()
        {
            CreateMap<BlogPost, BlogPostDto>()
               .ForMember(dto => dto.Id, opt => opt.MapFrom(src => src.Id.Value.ToString()))
               .ForMember(dto => dto.BlogHeader, opt => opt.MapFrom(src => src.BlogHeader.Value))
               .ForMember(dto => dto.BlogBody, opt => opt.MapFrom(src => src.BlogBody));
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
