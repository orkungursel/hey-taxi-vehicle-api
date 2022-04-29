using System.Diagnostics.CodeAnalysis;
using HeyTaxi.VehicleApi;

namespace HeyTaxi.VehicleService.Infrastructure.Clients;

public class UserServiceClient {
    private readonly UserService.UserServiceClient _client;

    public UserServiceClient(UserService.UserServiceClient client) {
        _client = client;
    }

    public async Task<UserInfo?> GetUserInfoAsync(string userId, CancellationToken cancellationToken)
    {
        GetUserInfoRequest request = new()
        {
            UserIds = { userId }
        };

        var response = await _client.GetUserInfoAsync(request, null, null, cancellationToken);
        
        var userInfo = response.Users.FirstOrDefault();

        return userInfo;
    }
}
