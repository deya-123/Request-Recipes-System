using OurRecipes.Models;
using OurRecipes.ViewModels;
using AutoMapper;


namespace OurRecipes.Services
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<UserViewModel, User>();
            CreateMap<TestimonialViewModel, Testimonial>()
            .ForMember(dest => dest.UserId, opt => opt.Ignore()) // Ignore mapping for UserId
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.TestimonialStatus, opt => opt.Ignore()) ;
            CreateMap<ContactViewModel, Contact>();
            CreateMap<RecipeCategoryViewModel, RecipeCategory>();
            CreateMap<UserProfileViewModel, User>().ForMember(dest => dest.UserPassword, opt => opt.Ignore()); ;
            CreateMap<User, UserProfileViewModel>();
            CreateMap<RecipeViewModel, Recipe>().ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
        }
    }
}
