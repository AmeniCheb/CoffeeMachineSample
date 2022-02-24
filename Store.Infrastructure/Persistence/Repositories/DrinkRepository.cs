using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Store.ApplicationCore.DTOs;
using Store.ApplicationCore.Interfaces;
using Store.Infrastructure.Persistence.Contexts;
using Store.ApplicationCore.Entities;
using Store.ApplicationCore.Utils;
using Store.ApplicationCore.Exceptions;

namespace Store.Infrastructure.Persistence.Repositories
{
    internal class DrinkRepository : IDrinkRepository
    {
        private readonly StoreContext storeContext;
        private readonly IMapper mapper;

        public DrinkRepository(StoreContext storeContext, IMapper mapper)
        {
            this.storeContext = storeContext;
            this.mapper = mapper;
        }
        public DrinkResponse CreateDrink(CreateDrinkRequest createDrinkRequest)
        {
            var drink = this.mapper.Map<Drink>(createDrinkRequest);

            drink.CreatedAt = drink.UpdatedAt = DateTime.Now;
            storeContext.Drinks.Add(drink);
            storeContext.SaveChanges();

            return mapper.Map<DrinkResponse>(drink);
        }

        public DrinkResponse GetDrinkByBadge(int badge)
        {
            var drink = this.storeContext.Drinks.FirstOrDefault(d=> d.Badge == badge);
            if (drink != null)
                return this.mapper.Map<DrinkResponse>(drink);
            else
                throw new NotFoundException();
        }

        public List<DrinkResponse> GetDrinks()
        {
            return this.storeContext.Drinks.Select(p => this.mapper.Map<DrinkResponse>(p)).ToList();
        }

        public DrinkResponse UpdateDrink(int badge, UpdateDrinkRequest request)
        {
            var drink = storeContext.Drinks.FirstOrDefault(d=>d.Badge == badge);
            if (drink != null)
            {
                drink.Type = request.Type;
                drink.SugarQt = request.SugarQt;
                drink.UseOwnMug = request.UseOwnMug;
                drink.UpdatedAt = DateTime.Now;

                this.storeContext.Update(drink);
                this.storeContext.SaveChanges();

                return this.mapper.Map<DrinkResponse>(drink);
            }

            throw new NotFoundException();
        }
    }
}
