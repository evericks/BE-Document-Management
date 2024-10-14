namespace Domain.Models.Authorization;

public class AuthenticationModel
{
    public string AccessToken { get; set; } = null!;
    public UserInformationModel User { get; set; } = null!;
}