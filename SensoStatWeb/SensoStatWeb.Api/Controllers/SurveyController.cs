using Microsoft.AspNetCore.Mvc;
using SensoStatWeb.Models.DTOs.Down;
using SensoStatWeb.Models.Entities;
using SensoStatWeb.Repository.Interfaces;
using System.Linq;

namespace SensoStatWeb.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class SurveyController : Controller
{
    private readonly ISurveyRepository _surveyRepository;
    private readonly IAdministratorRepository _administratorRepository;
    private readonly IUserRepository _userRepository;
    private readonly ISurveyStateRepository _surveyStateRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUserProductRepository _userProductRepository;

    public SurveyController(ISurveyRepository surveyRepository, IAdministratorRepository administratorRepository,IUserRepository userRepository,ISurveyStateRepository surveyStateRepository,IProductRepository productRepository,IUserProductRepository userProductRepository)
    {
        _surveyRepository = surveyRepository;
        _administratorRepository = administratorRepository;
        _userRepository = userRepository;
        _surveyStateRepository = surveyStateRepository;
        _productRepository = productRepository;
        _userProductRepository = userProductRepository;
    }

    [HttpGet]
    // GET: SurveyController
    public IActionResult Survey()
    {
        var result = _surveyRepository.GetAllSurveys();
        if(result == null)
        {
            return NotFound();
        }
        else
        {
            return Ok(result);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Survey([FromBody]SurveyCreationDTODown surveyCreationDTODown)
    {
        var survey = new Survey()
        {
            Name = surveyCreationDTODown.Name,
            CreatorId = 1,
            Administrator = _administratorRepository.GetAdministrator(1),
            CreationDate = DateTime.Now,
            SurveyState = await _surveyStateRepository.GetSurveyState(1),
            StateId = 1,
            Users = new List<User>(),
            Instructions = new List<Instruction>(),
            Questions = new List<Question>(),
            Products = new List<Product>(),
        };

        var result = await _surveyRepository.CreateSurvey(survey);

        foreach (var userProduct in surveyCreationDTODown.Users)
        {
            userProduct.Id = userProduct.Code + result.Id;
            userProduct.Answers = new List<Answer>();
            userProduct.Code = userProduct.Code;
            userProduct.UserProducts = new List<UserProduct>();
            userProduct.Survey = _surveyRepository.GetSurvey(result.Id);
            await _userRepository.CreateUser(userProduct);
        }

        foreach (var userProduct in surveyCreationDTODown.Products)
        {
            userProduct.Survey = _surveyRepository.GetSurvey(result.Id);
            userProduct.UserProducts = new List<UserProduct>();
            await _productRepository.CreateProduct(userProduct);
        }

        var users = await _userRepository.GetUsers();
        var products = await _productRepository.GetAllProducts();

        foreach (var userProduct in surveyCreationDTODown.UserProducts)
        {
            userProduct.User = users.Where(u => u.Id == userProduct.User.Code + result.Id).FirstOrDefault();
            userProduct.Product = products.FirstOrDefault(p => p.Code == userProduct.Product.Code);
            await _userProductRepository.CreateUserProduct(userProduct);
        }


        return result == null ? NotFound() : Ok(result);
    }

    [HttpPut]
    public IActionResult Survey([FromBody]Survey survey)
    {
        var result = _surveyRepository.UpdateSurvey(survey);
        if (result == null)
        {
            return NotFound();
        }
        else
        {
            return Ok(result);
        }
    }

    [HttpDelete]
    public IActionResult Survey(int id)
    {
        var result = _surveyRepository.DeleteSurvey(id);
        if(result.Result != true)
        {
            return Ok(result.Result);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet("UserId")]
    [ActionName("Survey")]
    public IActionResult GetSurveyByUserId([FromRoute] int userId)
    {
        var result = _surveyRepository.GetSurvey(userId);

        if (result == null)
        {
            return NotFound();
        }
        else
        {
            return Ok(result);
        }
    }

}
