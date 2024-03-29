using ICollection.DataAccess.Interfaces.Common;
using ICollection.DataAccess.Repositories.Common;
using ICollection.Service.Interfaces.Accounts;
using ICollection.Service.Interfaces.Admins;
using ICollection.Service.Interfaces.Collections;
using ICollection.Service.Interfaces.Comments;
using ICollection.Service.Interfaces.Common;
using ICollection.Service.Interfaces.CustomFields;
using ICollection.Service.Interfaces.Files;
using ICollection.Service.Interfaces.Items;
using ICollection.Service.Interfaces.Likes;
using ICollection.Service.Interfaces.Tags;
using ICollection.Service.Interfaces.Users;
using ICollection.Service.Services.Accounts;
using ICollection.Service.Services.Admins;
using ICollection.Service.Services.Collections;
using ICollection.Service.Services.Comments;
using ICollection.Service.Services.Common;
using ICollection.Service.Services.CustomFields;
using ICollection.Service.Services.Files;
using ICollection.Service.Services.Items;
using ICollection.Service.Services.Likes;
using ICollection.Service.Services.Tags;
using ICollection.Service.Services.Users;

namespace ICollection.Presentation.Configuration.LayerConfigurations
{
    public static class ServiceLayerConfiguration
    {
        public static void AddService(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ICollectionService,CollectionService>();
            services.AddScoped<ICommentService,CommentService>();
            services.AddScoped<ICustomFieldService,CustomFieldService>();
            services.AddScoped<IitemService,itemService>();
            services.AddScoped<ILikeService,LikeService>();
            services.AddScoped<ITagService,TagService>();
            services.AddScoped<IUserService, UserService>();

            services.AddMemoryCache();
            services.AddHttpContextAccessor();
            services.AddAutoMapper(typeof(MappingConfiguration));

        }
    }
}
