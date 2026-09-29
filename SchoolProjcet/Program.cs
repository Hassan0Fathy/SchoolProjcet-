
using School.AppContext;
using Microsoft.EntityFrameworkCore;
using SchoolProjcet.IRepos;
using SchoolProjcet.Repos;
using School.Models;
using SchoolProjcet.IGenericRepo;
using SchoolProjcet.UnitOfWorks;
namespace SchoolProjcet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped(typeof(IGeneric<>), typeof(GenericRepo<>));

            builder.Services.AddScoped<IRepos.IStudentRepo, StudentRepo>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IEnrollmentsRepo, EnrollmentsRepo>();

            builder.Services.AddScoped<ITeacherRepo, TeacherRepo>();
            builder.Services.AddScoped<ISubjectRepo, SubjectRepo>();
            builder.Services.AddScoped<IClassRoomRepo, ClassRoomRepo>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
