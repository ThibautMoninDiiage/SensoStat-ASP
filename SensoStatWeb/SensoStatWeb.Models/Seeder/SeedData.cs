using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SensoStatWeb.Models.Factories;
using SensoStatWeb.Models.Entities;

namespace SensoStatWeb.Models.Seeder
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new SensoStatDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<SensoStatDbContext>>()))
            {
                if (context.Administrators.Any())
                {
                    return; // DB a été seed
                }
                else
                {
                    context.Administrators.AddRange(CreateFactories.CreateAdmin());
                }
                if (context.SurveyStates.Any())
                {
                    return;
                }
                else
                {
                    context.SurveyStates.AddRange(CreateFactories.CreateSurveyStates());
                }
                if (context.Users.Any())
                {
                    return;
                }
                else
                {
                    context.Users.AddRange(CreateFactories.CreateUser());
                }
                if (context.Products.Any())
                {
                    return;
                }
                else
                {
                    context.Products.AddRange(CreateFactories.CreateProducts());
                }
                if (context.UserProducts.Any())
                {
                    return;
                }
                else
                {
                    context.UserProducts.AddRange(CreateFactories.LinkUserProducts());
                }
                if (context.Questions.Any())
                {
                    return;
                }
                else
                {
                    context.Questions.AddRange(CreateFactories.CreateQuestion());
                }
                if (context.Instructions.Any())
                {
                    return;
                }
                else
                {
                    context.Instructions.AddRange(CreateFactories.CreateInstructions());
                }
                if (context.Surveys.Any())
                {
                    return;
                }
                else
                {
                    context.Surveys.AddRange(CreateFactories.CreateSurvey());
                }
                if (context.SurveyInstructions.Any())
                {
                    return;
                }
                else
                {
                    context.SurveyInstructions.AddRange(CreateFactories.CreateSurveyInstruction());
                }

                context.SaveChanges();
            }
        }
    }
}
