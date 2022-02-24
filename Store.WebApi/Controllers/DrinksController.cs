using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.ApplicationCore.DTOs;
using Store.ApplicationCore.Exceptions;
using Store.ApplicationCore.Interfaces;

namespace Store.WebApi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class DrinksController : Controller
    {
        private readonly IDrinkRepository drinkRepository;

        public DrinksController(IDrinkRepository drinkRepository)
        {
            this.drinkRepository = drinkRepository;
        }
        [HttpGet]
        public ActionResult<List<DrinkResponse>> GetDrinks()
        {
            return Ok(this.drinkRepository.GetDrinks());
        }

        [HttpGet("{badge}")]
        public ActionResult GetDrinkByBadge(int badge)
        {
            try
            {
                var drink = drinkRepository.GetDrinkByBadge(badge);
                return Ok(drink);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
        [HttpPost]
        public ActionResult Create(CreateDrinkRequest request)
        {
            var drink = drinkRepository.CreateDrink(request);
            return Ok(drink);
        }

        [HttpPut("{badge}")]
        public ActionResult Update(int badge, UpdateDrinkRequest request)
        {
            try
            {
                var product = drinkRepository.UpdateDrink(badge, request);
                return Ok(product);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}
