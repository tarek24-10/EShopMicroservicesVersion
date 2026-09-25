namespace Ordering.Application.Dtos
{
    public record AddressDto(
        string FirstName,
        string LastName,
        string Email,
        string Country,
        string State,
        string AddressLine,
        string ZipCode
    );
}
