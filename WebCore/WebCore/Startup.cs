using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using Common;
using Common.Cache;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using WebCore.AOP;
using WebCore.Intrceptors;
using WebEf;
using WebServices;
using CoreRepository.EF;
using System.IO;
using AutoMapper;
using WebCore.Profile;
using WebCore.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Core;
using Core.Model;

namespace WebCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public  IConfiguration Configuration { get; }

        private List<Type> list_type = new List<Type>();

        /// <summary>
        /// onfigureServices方法是用来把services(各种服务, 例如identity, ef, mvc等等包括第三方的, 或者自己写的)加入(register)到container(asp.net core的容器)中去, 并配置这些services
        ///  // 运行时调用此方法。使用此方法向容器中添加服务。
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            GetSettings(services);
            //注册MVC到Container
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            SetCors(services);

            SetSwagger(services);

            //初始化autoMapper    并且注册AutoMapProfile  添加所有待映射的对象
            Mapper.Initialize(cfg => cfg.AddProfiles(typeof(AutoMapProfile)));
            //services.AddAutoMapper();

            SetAuthorazition(services);

            services.AddMvc();
           
            return new AutofacServiceProvider(SetAutoFac(services));
        }


        /// <summary>
        /// 方法是asp.net core程序用来具体指定如何处理每个http请求的, 例如我们可以让这个程序知道我使用mvc来处理http请求
        /// // 运行时调用此方法。使用此方法配置HTTP请求管道。
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //判断是否是开发环境
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // app.UseCors(options => options.WithOrigins("http://192.168.25.169:18888", "http://localhost:18888").AllowAnyHeader().AllowAnyMethod());
            app.UseCors("allDefault");
            app.UseStaticFiles();
            app.UseFileServer();

            //注册中间件
            app.Use(next =>
            {
                return async (context) =>
                {
                    string url = context.Request.Path;
                    await next(context);
                    //var   response = context.Response;
                    //if (response.Body.CanRead)
                    //{
                    //    byte[] bytes = new byte[response.Body.Length];
                    //    response.Body.Read(bytes, 0, (int)response.Body.Length);

                    //    string ss = Encoding.UTF8.GetString(bytes);
                    //}
                   
                };

            });

            UseSwagger(app);

            //自定义认证中间件
            // app.UseMiddleware<TokenAuth>();
            app.UseAuthentication();
            app.UseMvc();
            


        }


        #region 私有方法
        /// <summary>
        /// 获取appsettings  配置文件信息
        /// </summary>
        private  void GetSettings(IServiceCollection services)
        {
            //设置数据库连接字符候选
            AppSettingsHelpper.Configuration = Configuration;
            AppSettingsHelpper.ConnectionString = Configuration.GetSection("AppSettings:Sql:ConnectionString").Value;
            AppSettingsHelpper.DbType = Configuration.GetSection("AppSettings:Sql:DbType").Value;

            services.AddDbContext<EFDbContext>(options => options.UseSqlServer(AppSettingsHelpper.ConnectionString));
          
            //加载缓存配置
            list_type.Clear();
            if (Configuration.GetSection("AppSettings:AOP:LogAOP").GetValue<bool>("Enabled"))
                list_type.Add(typeof(CoreLogAOP));
            if (Configuration.GetSection("AppSettings:AOP:CacheAOP").GetValue<bool>("Enabled"))
                list_type.Add(typeof(CoreCacheAOP));
            if (Configuration.GetSection("AppSettings:AOP:TranAop").GetValue<bool>("Enabled"))
                list_type.Add(typeof(CoreTranAOP));
        }

        /// <summary>
        /// 设置跨域请求
        /// </summary>
        /// <param name="services"></param>
        private   void   SetCors(IServiceCollection services)
        {
            services.AddCors(Option =>
            {
                Option.AddPolicy("allDefault", policy =>
                 {
                     policy.AllowAnyOrigin()
                         .AllowAnyHeader()
                         .AllowAnyMethod()
                         .AllowCredentials();

                 });
            });

            services.AddCors(Option =>
            {
                Option.AddPolicy("default", policy =>
                {
                    policy.WithOrigins("http://localhost:18888")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();

                });
            });
        }

        /// <summary>
        /// 设置授权
        /// </summary>
        private  void SetAuthorazition(IServiceCollection services)
        {

            var symmetricKeyAsBase64 = Configuration.GetSection("AppSettings:Token:secret").Value;
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(symmetricKeyAsBase64));

            var issuer = AppSettingsHelpper.Configuration.GetSection("AppSettings:Token:issuer").Value;
            var audience = AppSettingsHelpper.Configuration.GetSection("AppSettings:Token:audience").Value;

            //2.1【认证】
            services.AddAuthentication(x =>
            {
                //看这个单词熟悉么？没错，就是上边错误里的那个。
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(o =>
             {
                 o.TokenValidationParameters = new TokenValidationParameters
                 {
                     
                     IssuerSigningKey = signingKey,//参数配置在下边
                     ValidIssuer = issuer,//发行人
                     ValidAudience = audience,//订阅人
                     ClockSkew = TimeSpan.Zero,//偏移时间
                 };
                 #region  注释
                 /***********************************TokenValidationParameters的参数默认值***********************************/
                 //需要签名令牌
                 // RequireSignedTokens = true,
                 //保存登录令牌
                 // SaveSigninToken = false,
                 //验证参与者
                 // ValidateActor = false,
                 // 将下面两个参数设置为false，可以不验证Issuer和Audience，但是不建议这样做。
                 //验证观众
                 // ValidateAudience = true,
                 //验证发布者
                 // ValidateIssuer = true, 
                 //是否验证签名密钥
                 // ValidateIssuerSigningKey = false,
                 // 是否要求Token的Claims中必须包含Expires
                 // RequireExpirationTime = true,
                 // 允许的服务器时间偏移量
                 // ClockSkew = TimeSpan.FromSeconds(300),
                 // 是否验证Token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比
                 // ValidateLifetime = true
                 /***************************************************************************************************************/
                 #endregion
             });

            //services.AddAuthorization(options =>
            //{
            //    //添加授权策略
            //    options.AddPolicy("System", policy => policy.RequireClaim("SystemType").Build());
            //    options.AddPolicy("Client", policy => policy.RequireClaim("ClientType").Build());
            //    options.AddPolicy("Admin", policy => policy.RequireClaim("AdminType").Build());
            //});
        }
        #region Swagger
        /// <summary>
        /// SetSwagger
        /// </summary>
        /// <param name="services"></param>
        private void SetSwagger(IServiceCollection services)
        {

            services.AddSwaggerGen(c =>
            {
                //定义一个或多个由Swagger生成器创建的文档
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v0.1.0",
                    Title = "Blog.Core API",
                    Description = "框架说明文档",
                    TermsOfService = "None",
                    Contact = new Swashbuckle.AspNetCore.Swagger.Contact { Name = "Blog.Core", Email = "Blog.Core@xxx.com", Url = "https://www.jianshu.com/u/94102b59cc2a" }
                });
                //获取或设置用于访问Swagger UI的路由前缀
                //  string basePath2 = AppDomain.CurrentDomain.BaseDirectory;
                var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath + @"SwaggerXml\";
                //默认的第二个参数是false，这个是controller的注释，记得修改
                string xmlPath = basePath + "WebCore.xml";
                c.IncludeXmlComments(xmlPath, true);
                string xmlModelPath = basePath + "CoreDto.xml";
                c.IncludeXmlComments(xmlModelPath, true);

                //添加header验证信息
                //c.OperationFilter<SwaggerHeader>();

                var security = new Dictionary<string, IEnumerable<string>> { { AppSettingsHelpper.SecurityName, new string[] { } }, };
                //添加一个必须的全局安全信息，和AddSecurityDefinition方法指定的方案名称要一致，这里是Bearer。
                c.AddSecurityRequirement(security);
                c.AddSecurityDefinition(AppSettingsHelpper.SecurityName, new ApiKeyScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 参数结构: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = "header",//jwt默认存放Authorization信息的位置(请求头中)
                    Type = "apiKey"
                });

            });
        }

        /// <summary>
        /// 使用配置swaager
        /// </summary>
        /// <param name="app"></param>
        private void UseSwagger(IApplicationBuilder app)
        {
            app.UseSwagger();
            //添加Swagger JSON端点。可以是完全限定的或相对于UI页
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelp V1");
                //获取或设置用于访问Swagger UI的路由前缀
                //路径配置，设置为空，表示直接访问该文件，
                //路径配置，设置为空，表示直接在根域名（localhost:8001）访问该文件,注意localhost:8001/swagger是访问不到的，
                //这个时候去launchSettings.json中把"launchUrl": "swagger/index.html"去掉， 然后直接访问localhost:8001/index.html即可
                c.RoutePrefix = "";
            });
        }
        #endregion
        /// <summary>
        /// 设置依赖注入框架AutoFac
        /// </summary>
        /// <param name="services"></param>
        private IContainer SetAutoFac(IServiceCollection services)
        {

            //备注
            //AddTransient瞬时模式：每次请求，都获取一个新的实例。即使同一个请求获取多次也会是不同的实例
            //AddScoped：每次请求，都获取一个新的实例。同一个请求获取多次会得到相同的实例
            //AddSingleton单例模式：每次都获取同一个实例
           // services.AddTransient(typeof(IEFRepository<,>),typeof(EFBaseRepository<,>));

            //实例化 AutoFac  容器   
            var builder = new ContainerBuilder();
            //注册log拦截器
            builder.RegisterType<CoreLogAOP>().OnRegistered(x=>
            {
                var path = Directory.GetCurrentDirectory() + @"\Log";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
            });

            //注入缓存管理
            services.AddScoped(typeof(ICache), typeof(MemoryCache));
            builder.RegisterType<CoreCacheAOP>();

            //注入数据库以便让server 可以默认开启事务
            builder.RegisterType<EFDbContext>();
            builder.RegisterType<CoreTranAOP>();

            //注册整个dll   //要记得!!!这个注入的是实现类层，不是接口层！
            Assembly assemblyService = Assembly.GetAssembly(typeof(BaseService));
            //指定已扫描程序集中的类型注册为提供所有其实现的接口。
            var Registerbuilder = builder.RegisterAssemblyTypes(assemblyService).AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            //拦截器
            if (list_type != null && list_type.Count > 0)
                Registerbuilder.InterceptedBy(list_type.ToArray()).EnableInterfaceInterceptors();

            //注册一个通用类型
            builder.RegisterGeneric(typeof(EFBaseRepository<,>)).As(typeof(IEFRepository<,>)).InstancePerLifetimeScope();
            //将services填充到Autofac容器生成器中
            builder.Populate(services);


           // //泛型注册,可以通过容器返回List<T> 如:List<string>,List<int>等等
           //// builder.RegisterGeneric(typeof(Aaa<,>)).As(typeof(Iaaa<,>)).InstancePerLifetimeScope();
           // using (IContainer container = builder.Build())
           // {
           //     IEFRepository<Advertisement, int> ListString = container.Resolve<IEFRepository<Advertisement, int>>();
           //     Advertisement aa = new Advertisement();
           //     aa.Id = 1234;
           //     ListString.Delete(aa);
           // }







            //使用已进行的组件登记创建新容器
            var ApplicationContainer = builder.Build();
            return ApplicationContainer;
        }

        #endregion
    }

    public class Aaa<TEntity, TPrimaryKey> : Iaaa<TEntity, TPrimaryKey> 
        where TEntity : class, IEntity<TPrimaryKey>
    {
        public Dictionary<string, TEntity> Add(TEntity Tkey)
        {
            Dictionary<string, TEntity> dic = new Dictionary<string, TEntity>();
            dic.Add("aa", Tkey);
            return dic;
        }
    }

    public interface Iaaa<TEntity, TPrimaryKey> 
    {
        Dictionary<string, TEntity> Add(TEntity Tkey);
    }

    public  class  asd : IEntity<int>
    {
        public int Id { get; set; }
    }
}
