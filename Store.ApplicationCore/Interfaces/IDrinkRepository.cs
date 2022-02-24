using System;
using System.Collections.Generic;
using System.Text;
using Store.ApplicationCore.DTOs;

namespace Store.ApplicationCore.Interfaces
{
    public interface IDrinkRepository
    {
        List<DrinkResponse> GetDrinks();
        DrinkResponse GetDrinkByBadge(int badge);
        DrinkResponse CreateDrink(CreateDrinkRequest createDrinktRequest);
        DrinkResponse UpdateDrink(int badge, UpdateDrinkRequest request);
    }
}
