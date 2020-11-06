using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdvertApi.Models;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using AutoMapper;

namespace AdvertApi.Services
{
    public class DynamoDbAdvertStorage : IAdvertStorageService
    {
        private readonly IMapper _mapper;
        private readonly AmazonDynamoDBClient _client;

        public DynamoDbAdvertStorage(IMapper mapper, IAmazonDynamoDB dynamoDb)
        {
            _mapper = mapper;
            _client = (AmazonDynamoDBClient) dynamoDb;
        }
        public async Task<string> Add(AdvertModel model)
        {
            var dbModel = _mapper.Map<AdvertDbModel>(model);
            dbModel.Id = new Guid().ToString();
            dbModel.CreationDateTime = DateTime.UtcNow;
            dbModel.Status = AdvertStatus.Pending;
            
          
                using (var context = new DynamoDBContext(_client))
                {
                    await context.SaveAsync(dbModel);
                }
            

            return dbModel.Id;
        }
        

        public async Task Confirm(ConfirmAdvertModel model)
        {
            
                using (var context = new DynamoDBContext(_client))
                {
                    var record = await context.LoadAsync<AdvertDbModel>(model.Id);
                    if (record == null)
                    {
                        throw new KeyNotFoundException($"A record with Id={model.Id} was not found");
                    }

                    if (model.Status == AdvertStatus.Active)
                    {
                        record.Status = AdvertStatus.Active;
                        await context.SaveAsync(record);
                    }
                    else
                    {
                        await context.DeleteAsync(record);
                    }
                }
            
        }

        public async Task<bool> CheckHealthAsync()
        {
            
                var request = new ListTablesRequest
                {
                    Limit = 10
                };
                var response = await _client.ListTablesAsync(request);
                var results = response.TableNames;
                var tableData = await _client.DescribeTableAsync("adverts");
                return string.Compare(tableData.Table.TableStatus, "active", true) == 0;
            
        }
    }
}