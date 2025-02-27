using FluentValidation;
using FluentValidation.AspNetCore;
using LSDCS.Entity.DTOs.MailLog;
using LSDCS.Service.FluenValidations;
using LSDCS.Service.Services.Abstractions;
using LSDCS.Service.Services.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Reflection;

namespace LSDCS.Service.Extensions
{
    public static class ServiceLayerExtension
    {

        public static IServiceCollection LoadServiceLayerExtension(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            // Assembly.GetExecutingAssembly() ile mevcut çalışan assembly'yi alıyoruz.
            var assembly = Assembly.GetExecutingAssembly();

            // HttpContextAccessor servisini singleton olarak ekliyoruz. Bu adım aynı anda iki kez yapılmış, birini kaldırmanız daha temiz olacaktır.
        

            // Çeşitli servisleri scoped olarak kaydediyoruz. Bu, her bir web isteği için bu servislerden yeni bir örnek oluşturulacağı anlamına gelir.
            services.AddScoped<IMailLogService, MailLogService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IMatterService, MatterService>();
            services.AddScoped<IMailRecipintService, MailRecipintService>();
            services.AddScoped<IRecipientService, RecipientService>();
            services.AddScoped<IMailRelationsService, MailRelationsService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDashBoardService, DashBoardService>();
            services.AddScoped<IDocumentService, DocumentService>();

            // AutoMapper'ı assembly bazında ekliyoruz. Bu, belirtilen assembly içindeki tüm AutoMapper profillerini otomatik olarak yükleyecektir.
            services.AddAutoMapper(assembly);

            // Controller'lar ve View'lar için destek ekliyor ve FluentValidation ile validasyon kurallarını yapılandırıyoruz.
            services.AddControllersWithViews().AddFluentValidation(opt =>
            {
                // MailLogValidator sınıfını içeren assembly'den validatörleri kaydediyoruz.
                opt.RegisterValidatorsFromAssemblyContaining<MailLogValidator>();

                // DataAnnotations validasyonunu devre dışı bırakıyoruz. Bu, FluentValidation ile özel validasyonlarımızı kullanacağımız anlamına gelir.
                opt.DisableDataAnnotationsValidation = true;

                // Validasyon mesajlarının Türkçe olmasını sağlıyoruz.
                opt.ValidatorOptions.LanguageManager.Culture = new CultureInfo("tr");
            });

            return services;

        }

    }
}
