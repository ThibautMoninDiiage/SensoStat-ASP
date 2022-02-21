using SensoStatWeb.Models.Entities;

namespace SensoStatWeb.Models.Factories
{
    public class CreateFactories
    {

        public static List<Question>? Questions { get; set; }
        public static List<Product>? Products { get; set; }

        public static List<Survey>? Surveys { get; set;}

        public static List<Administrator> CreateAdmin()
        {
            var administrators = new List<Administrator>()
            {
                new Administrator{Id = 1,FirstName = "Tristan",LastName = "Devoille",UserName = "td",Password = "root"},
                new Administrator{Id = 2,FirstName = "Thomas",LastName = "Bernard",UserName = "tb",Password = "root"},
                new Administrator{Id = 3,FirstName = "Thibaut",LastName = "Monin",UserName = "tm",Password = "root"},
                new Administrator{Id = 4,FirstName = "Baptiste",LastName = "Rameau",UserName = "br",Password = "root"},
            };

            return administrators;
        }

        public static List<User> CreateUser()
        {
            var users = new List<User>()
            {
                new User{Id = 5,FirstName ="Alexis",LastName ="Desgranges"},
                new User{Id = 6,FirstName ="Clement",LastName ="Delarue"},
                new User{Id = 7,FirstName ="Jean-Christophe",LastName ="Pouzin"},
            };

            return users;
        }

        public static List<SurveyState> CreateSurveyStates()
        {
            var surveyStates = new List<SurveyState>()
            {
                new SurveyState{Id = 1,Libelle = "Pas déployé"},
                new SurveyState{Id = 2,Libelle = "Déployé"},
            };

            return surveyStates;
        }

        public static List<Question> CreateQuestion()
        {
            var questions = new List<Question>()
            {
                new Question{Id = 1,Libelle = "Qu'avez vous pensé de ce produit?",Position = 2},
                new Question{Id = 2,Libelle = "Etait-il croustillant?",Position = 3},
                new Question{Id = 3,Libelle = "Quelles sont vos sensations ressenties?", Position = 4},
                new Question{Id = 4,Libelle = "Avez vous des remarques à faire ?", Position = 5},
            };

            Questions = questions;

            return questions;
        }

        public static List<Instruction> CreateInstructions()
        {
            var instructions = new List<Instruction>()
            {
                new Instruction() { Id = 1, Libelle = "Goutez la chips" },
                new Instruction() { Id = 2, Libelle = "Merci pour votre test" },
            };

            return instructions;
        }

        public static Survey CreateSurvey()
        {
            Surveys = new List<Survey>();
            var admin = CreateAdmin().FirstOrDefault();
            var user = CreateUser().Find(u => u.Id == 6);
            var survey = new Survey()
            {
                Id = 1,
                Name = "Les chips",
                CreatorId = admin.Id,
                CreationDate = DateTime.Now,
                StateId = CreateSurveyStates().FirstOrDefault().Id,
                UserId = user.Id,
                Questions = Questions, 
            };

            Surveys.Add(survey);

            return survey;
        }

        public static List<SurveyInstruction> CreateSurveyInstruction()
        {
            List<SurveyInstruction> surveyInstructions = new List<SurveyInstruction>()
            {
                new SurveyInstruction() { InstructionId = 1,Position = 1,SurveyId = 1},
                new SurveyInstruction() { InstructionId = 2,Position = 6,SurveyId = 1},
            };

            return surveyInstructions;
        }

        public static List<Product> CreateProducts()
        {
            var products = new List<Product>()
            {
                new Product{Id = 1,Code = 019,Surveys = Surveys},
                new Product{Id = 2,Code = 150,Surveys = Surveys},
                new Product{Id = 3,Code = 300,Surveys = Surveys},
            };

            Products = products;

            return products;
        }

        public static List<UserProduct> LinkUserProducts()
        {
            var users = CreateUser();
            var products = CreateProducts();
            int index = 0;
            int[] position = new int[] {0,1,2};
            Random rnd = new Random();
            List<UserProduct> userProducts = new List<UserProduct>();
            foreach (var user in users)
            {
                List<UserProduct> userProducts1 = new List<UserProduct>();
                foreach (var product in products)
                {
                    index = position[rnd.Next(position[0], position[position.Count() - 1])];
                    var cont = userProducts1.GroupBy(up => up.Position);
                    while (cont.Count() > 2)
                    {
                        index = position[rnd.Next(position[0], position[position.Count() - 1])];
                        cont = userProducts1.GroupBy(up => up.Position);
                    }
                    userProducts1.Add(new UserProduct { ProductId = product.Id, UserId = user.Id,Position = index});
                }
                userProducts.AddRange(userProducts1);
            }
            return userProducts;
        }
    }
}
