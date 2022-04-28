using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Web;
using BaseCorporate.Service.Abstract;
using BaseCorporate.Service.Concrete;
using BaseCorporate.Service.Model;
using BaseCorporate.Utility.Helper;
using BaseCorporate.Utility.Model;
using BaseCorporate.Web.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;

namespace BaseCorporate.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions().Configure<AppSettings>(Configuration);
            var options = services.BuildServiceProvider().GetService<IOptions<AppSettings>>()?.Value;
            AppParameter.AppSettings = options;
            if (AppParameter.AppSettings != null)
            {
                AppParameter.AppSettings.ConnectionString = options?.ConnectionString;
            }
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IMenuGroupService, MenuGroupService>();
            services.AddScoped<IMenuItemService, MenuItemService>();
            services.AddScoped<IMenuSubItemService, MenuSubItemService>();
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<ITopicService, TopicService>();
            services.AddScoped<IPageNotFoundLogService, PageNotFoundLogService>();
            services.AddScoped<IRedirectRecordService, RedirectRecordService>();
            services.AddScoped<IErrorLogService, ErrorLogService>();
            services.AddScoped<IUrlRecordService, UrlRecordService>();

            services.AddSingleton<RouteValueTransformer>();
            services.AddHttpContextAccessor();
            services.AddMvc().AddSessionStateTempDataProvider();
            services.AddSession();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IServiceProviderProxy, HttpContextServiceProviderProxy>();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            if (AppParameter.AppSettings is { ResetDatabase: true })
            {
                TestData.Builder();
            }
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new ThemeableViewLocationExpander());
            });

            var settingService = services.BuildServiceProvider().GetService<ISettingService>();
            AppParameterBinding.AllSetting(settingService);
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider sp)
        {
            ServiceLocator.Initialize(sp.GetService<IServiceProviderProxy>());
            AppParameter.RobotsTxtPath = $"{env.ContentRootPath}/robots.txt";
            AppParameter.ContentRootPath = env.ContentRootPath;
            //app.UseDeveloperExceptionPage();
            //if (env.IsDevelopment())//Geliþtirme tamamlanana kadar kapatýldý.
            //{
            //  
            //}
            //else
            //{
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.ContentType = "text/html";
                    context.Response.StatusCode = 500;
                    var referrerUrl = context.Request.Headers["referer"];
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                    var errorLog = new ErrorLogModel
                    {
                        PageUrl = exceptionHandlerPathFeature.Path,
                        ReferrerUrl = referrerUrl,
                        ExceptionType = exceptionHandlerPathFeature?.Error?.Source?.GetType()?.FullName,
                        ExceptionMessage = exceptionHandlerPathFeature?.Error?.Message,
                        UserIp = context.Request.HttpContext.Connection.RemoteIpAddress.ToString()
                    };

                    var errorLogService = context.Request.HttpContext.RequestServices.GetService<IErrorLogService>();
                    errorLogService?.Add(errorLog);

                    if (exceptionHandlerPathFeature.Path.Contains("/Admin/"))
                    {
                        context.Response.Redirect("/hata");
                    }
                    else
                    {
                        context.Response.Redirect("/hata");
                    }
                });
            });

            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    var userAgent = context.Request.Headers["User-Agent"];
                    var referrerUrl = context.Request.Headers["referer"];
                    var pageUrl = $"{context.Request.Path}{HttpUtility.UrlDecode(context.Request.QueryString.Value)}";

                    var redirectRecordService = context.Request.HttpContext.RequestServices.GetService<IRedirectRecordService>();
                    RedirectRecordModel redirectRecord = null;
                    if (redirectRecordService != null)
                    {
                        redirectRecord = redirectRecordService.Get(pageUrl);
                    }

                    if (redirectRecord != null && redirectRecord.Id > 0)
                    {
                        redirectRecord.RedirectCount++;
                        redirectRecordService.AddOrUpdate(redirectRecord);

                        context.Response.StatusCode = 301;
                        context.Response.Redirect(redirectRecord.NewUrl, true);
                    }
                    else
                    {
                        var pageNotFoundLogService = context.Request.HttpContext.RequestServices.GetService<IPageNotFoundLogService>();

                        if (pageNotFoundLogService != null)
                        {
                            var pageNotFoundLog = pageNotFoundLogService.Get(pageUrl);
                            if (pageNotFoundLog == null || pageNotFoundLog.Id == 0)
                            {
                                pageNotFoundLog = new PageNotFoundLogListItem
                                {
                                    PageUrl = pageUrl,
                                    ReferrerUrl = referrerUrl,
                                    UserIp = context.Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                                    UserAgent = userAgent
                                };
                            }

                            pageNotFoundLog.Count++;
                            pageNotFoundLog.UpdatedAt = DateTime.Now;
                            pageNotFoundLogService.AddOrUpdate(pageNotFoundLog);
                        }
                        context.Request.Path = "/Shared/PageNotFoundLog";
                        await next();
                    }
                }
            });
            app.UseHsts();
            //}

            app.UseSession();
            app.UseHttpsRedirection();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "Assets")),
                RequestPath = "/Assets"
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "Areas/Admin/Assets")),
                RequestPath = "/Areas/Admin/Assets"
            });
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "Themes")),
                RequestPath = "/Themes"
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse =
                    response =>
                    {
                        var path = response.File.PhysicalPath;
                        if (!path.EndsWith(".css") && !path.EndsWith(".js") && !path.EndsWith(".gif") && !path.EndsWith(".jpg") && !path.EndsWith(".png") && !path.EndsWith(".svg")) return;
                        var maxAge = new TimeSpan(1, 0, 0, 0);
                        response.Context.Response.Headers.Append("Cache-Control", "max-age=" + maxAge.TotalSeconds.ToString("0"));
                    }
            });

            app.UseCookiePolicy();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute("Admin", "Admin", "Admin/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapAreaControllerRoute("ElFinderThumbnail", "Admin", "/admin/elfinder/thumb/{hash}", new { controller = "ElFinder", action = "Thumbs" });
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("ErrorPage", "hata", new { controller = "Shared", action = "Error" });
                endpoints.MapControllerRoute("ContactUs", "iletisim", new { controller = "Shared", action = "ContactUs" });
                endpoints.MapControllerRoute("AboutUs", "hakkimizda", new { controller = "Shared", action = "AboutUs" });
                endpoints.MapControllerRoute("Search", "ara", new { controller = "Article", action = "Search" });
                endpoints.MapControllerRoute("Archive", "arsiv", new { controller = "Article", action = "Archive" });
                endpoints.MapControllerRoute("ArchiveList", "arsiv/{year}/{month}", new { controller = "Article", action = "ArchiveList" });
                endpoints.MapControllerRoute("SiteMap", "sitemap.xml", new { controller = "Shared", action = "SiteMap" });
                endpoints.MapControllerRoute("Rss", "rss", new { controller = "Shared", action = "Rss" });
                endpoints.MapControllerRoute("RobotsTxt", "robots.txt", new { controller = "Shared", action = "RobotsTxt" });
                endpoints.MapControllerRoute("FaviconIco", "favicon.ico", new { controller = "Shared", action = "FaviconIco" });
               
                endpoints.MapDynamicControllerRoute<RouteValueTransformer>("{slug}/{page?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
