using AutoMapper;
using CMS_RestAPI.DataAccessLayer.Context;
using CMS_RestAPI.DataAccessLayer.Repositories.Concrete.EntityTypeRepositories;
using CMS_RestAPI.DataAccessLayer.Repositories.Interfaces.EntityTypeRepositories;
using CMS_RestAPI.UI.Mapper;
using CMS_RestAPI.UI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CMS_RestAPI.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
           
            services.AddRouting(x => x.LowercaseUrls = true);

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddAutoMapper(typeof(AppUserMapper));
            services.AddAutoMapper(typeof(CategoryMapper));
            services.AddAutoMapper(typeof(PageMapper));
            services.AddAutoMapper(typeof(ProductMapper));

            services.AddScoped<IAppUserRepository, AppUserRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IPageRepository, PageRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();


            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("CMS API", new OpenApiInfo()
                {
                    Title = "CMS API",
                    Version = "V.1",
                    Description = "CMS API",
                    Contact = new OpenApiContact()
                    {
                        Email = "samettteraslan@gmail.com",
                        Name = "Samet ERASLAN",
                        Url = new Uri("https://github.com/sameteeraslann")
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "MIT Licance",
                        Url = new Uri("https://github.com/sameteeraslann")
                    }
                });

                var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommnetFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
                options.IncludeXmlComments(xmlCommnetFullPath);
            });

            // API'ýn sahib olduðu yetenekler yani Controller içerisindeki Action Metodlarýmýza yazdýðýmýz summary yani özet bilgilerin Swagger UI aracýnda gözükmesi için yapýlan bir konfigurasyon.

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.SecretKey);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            //CMS Rest API farklý bir domainde, API'ye request atan web siteside haliyle farklý bir domainde olacaktýr. Farklý origin'lerde kaynaklarda bulunan bu web platformlarýn saðlýklý bir þekilde iletiþim kurmasý için API'ye request atan web sitelerini tanýtmamýz gerekmektedir. Bunun için hiyerarþik olarak bulunan "UseCors" middleware kullanýlacaktýr. Özellikle veritabanýna varlýk insert etmek istediðimizde yada var olan bir valýk üzerinde deðiþiklik yapmak istediðimizde atacaðýmýz requestlerin baþarýlý olmasý için aþaðýda iþlemin yapýlmasý gerekmektedir.
            app.UseCors(options => options
                .AllowAnyOrigin() // Request atan web projesinin bulunduðu domain bilgisi, bu method içerisinde herhangi bir domain bilgisi verilmezse world wide web içerisinde herhangi bir alan adýna sahip web sitesi bize request atabilir.
                .AllowAnyMethod() // Hangi methodlara izin verildiði
                .AllowAnyHeader() // Hangi request header'larýna izin verildiði adým adým burdaki middleware'da titiz bir þekilde belirlenir.

            //Standart .Net içerisinde de ayný mantýk bulunmaktadýr. Standart .Net içerisinde Web Config içerisinde genel ayarlar yapýlabilindiði gibi Controller içerisine girerek action methodlara attribute olarakta bu origin bilgileri verile bilir. Method bu ayarlar verilebilir.

            //Asp .Net Core için [EnableCors] attribute vasýtasýyla yapýlýr.

            //Bu global ayarlar controller bazýnda ve action method bazýnda yapýlmaktadýr.
            );

            app.UseSwagger();
            app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/CMS API/swagger.json", "CMS API"); });

            app.UseAuthentication();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
    }
}
