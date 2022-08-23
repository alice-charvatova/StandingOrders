using StandingOrders.API.Models.Dto;


namespace StandingOrders.API.Services
{
    public interface IAuthorizationService
    {
        int GenerateCoordinate();

        bool ValidatePinCode(AuthorizationDto dto);


    }
}
