using Rentaly.BusinessLayer.Abstract;
using Rentaly.BusinessLayer.Concrete;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DataAccessLayer.Concrete;
using Rentaly.DataAccessLayer.EntityFramework;
using Rentaly.BusinessLayer.EmailService;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// DbContext kaydý
builder.Services.AddDbContext<RentalyContext>();
// AutoMapper kaydý
builder.Services.AddAutoMapper(typeof(Rentaly.BusinessLayer.Mapping.GeneralMapping));
// DAL Kayýtlarý
builder.Services.AddScoped<IBranchDal, BranchRepository>();
builder.Services.AddScoped<IBrandDal, BrandRepository>();
builder.Services.AddScoped<ICarDal, CarRepository>();
builder.Services.AddScoped<ICarModelDal, CarModelRepository>();
builder.Services.AddScoped<ICategoryDal, CategoryRepository>();
builder.Services.AddScoped<ICustomerDal, CustomerRepository>();
builder.Services.AddScoped<IRentalDal, RentalRepository>();
builder.Services.AddScoped<IProcessDal, ProcessRepository>();
builder.Services.AddScoped<IOurFutureDal, OurFutureRepository>();
builder.Services.AddScoped<IStatisticDal, StatisticRepository>();
builder.Services.AddScoped<IAwardDal, AwardRepository>();
builder.Services.AddScoped<ITestimonialDal, TestimonialRepository>();
builder.Services.AddScoped<IFAQDal, FAQRepository>();

// Business Kayýtlarý
builder.Services.AddScoped<IBranchService, BranchManager>();
builder.Services.AddScoped<IBrandService, BrandManager>();
builder.Services.AddScoped<ICarService, CarManager>();
builder.Services.AddScoped<ICarModelService, CarModelManager>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<ICustomerService, CustomerManager>();
builder.Services.AddScoped<IRentalService, RentalManager>();
builder.Services.AddScoped<IProcessService, ProcessManager>();
builder.Services.AddScoped<IOurFutureService, OurFutureManager>();
builder.Services.AddScoped<IStatisticService, StatisticManager>();
builder.Services.AddScoped<IAwardService, AwardManager>();
builder.Services.AddScoped<ITestimonialService, TestimonialManager>();
builder.Services.AddScoped<IFAQService, FAQManager>();

builder.Services.AddScoped<IEmailService, Rentaly.BusinessLayer.EmailService.EmailService>();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/Home/NotFoundPage");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Admin Area route
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();