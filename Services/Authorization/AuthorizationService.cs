using StandingOrders.API.Models.Dto;
using System;

namespace StandingOrders.API.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private int[,] gridCard = new int[6, 6];

        public AuthorizationService()
        {
            gridCard = FillGridCard();
        }

        public int GenerateCoordinate()
        {
            Random rnd = new Random();
            string coordinate = "";

            for (int i = 0; i < 2; i++)
            {
                int number = rnd.Next(1, 6);
                coordinate += number.ToString();
            }

            return Int32.Parse(coordinate);
        }

        public bool ValidatePinCode(AuthorizationDto authorizationDto)
        {   
            int firstNumber = Int32.Parse(authorizationDto.Coordinate.ToString().Substring(0, 1));
            int secondNumber = Int32.Parse(authorizationDto.Coordinate.ToString().Substring(1, 1));
            int codeFromGrid = gridCard[firstNumber, secondNumber];

            return (codeFromGrid == authorizationDto.PinCode);
        }

        private int[,] FillGridCard()
        {

            for (int i = 1; i <= 5; i++)
            {
                for (int j = 1; j <= 5; j++)
                {
                    gridCard[i, j] = Int32.Parse($"{i}{i}{j}{j}");
                }
            }
            return gridCard;  //Matrix positions where one of indexes is 0 remain empty.
        }
    }
}
