using Azure;
using Azure.Data.Tables;
using HexMaster.RedisCache.Abstractions;
using Microsoft.Extensions.Options;
using PollStar.Core.Configuration;
using PollStar.Users.Abstractions.DataTransferObjects;
using PollStar.Users.Abstractions.Repositories;
using PollStar.Users.Repositories.Entities;
using Constants = HexMaster.RedisCache.Abstractions.Constants;

namespace PollStar.Users.Repositories;

public class PollStarUsersRepository : IPollStarUsersRepository
{
    private readonly ICacheClient _cacheClient;
    private TableClient _tableClient;
    private const string TableName = "users";
    private const string PartitionKey = "user";

    public async Task<UserDto> GetAsync(Guid userId)
    {
        var redisCacheKey = $"users:{userId}";
        var userEntity =  await _cacheClient.GetOrInitializeAsync(() => GetUserByUserIsAsync(userId), redisCacheKey);
        return new UserDto
        {
            UserId = Guid.Parse(userEntity.RowKey)
        };
    }
    public async Task<bool> CreateAsync(Guid userId)
    {
        var entity = new UserTableEntity
        {
            PartitionKey = PartitionKey,
            RowKey = userId.ToString(),
            Timestamp = DateTimeOffset.UtcNow,
            ETag = ETag.All
        };
        var response = await _tableClient.AddEntityAsync(entity);
        return !response.IsError;
    }

    private async Task<UserTableEntity> GetUserByUserIsAsync(Guid userId)
    {
        var response = await _tableClient.GetEntityAsync<UserTableEntity>(PartitionKey, userId.ToString());
        return response.Value;
    }

    public PollStarUsersRepository(IOptions<AzureStorageConfiguration> azureStorageOptions, ICacheClientFactory cacheClientFactory)
    {
        _cacheClient = cacheClientFactory.CreateClient(Constants.DefaultCacheClientName);
        var accountName = azureStorageOptions.Value.StorageAccount;
        var accountKey = azureStorageOptions.Value.StorageKey;
        var storageUri = new Uri($"https://{accountName}.table.core.windows.net");
        _tableClient = new TableClient(
            storageUri,
            TableName,
            new TableSharedKeyCredential(accountName, accountKey));
    }

}