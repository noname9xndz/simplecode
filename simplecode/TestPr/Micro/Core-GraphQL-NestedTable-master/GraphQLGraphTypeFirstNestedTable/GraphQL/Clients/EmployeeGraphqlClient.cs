using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Client;
//using GraphQL.Common.Request;
using GraphQLGraphTypeFirstNestedTable.Models;
using GraphQLGraphTypeFirstNestedTable.Models.Responses;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace GraphQLGraphTypeFirstNestedTable.GraphQL.Clients
{
    //public class EmployeeGraphqlClient : IEmployeeGraphqlClient
    //{
    //    private readonly GraphQLClient _graphQLClient;
    //    private readonly HttpClient _httpClient;
    //    private readonly IConfiguration _configuration;

    //    public EmployeeGraphqlClient(GraphQLClient graphQLClient, HttpClient httpClient, IConfiguration configuration)
    //    {
    //        _graphQLClient = graphQLClient;
    //        _httpClient = httpClient;
    //        _configuration = configuration;
    //    }

    //    public async Task<ResponseModel<List<Employee>>> GetAllEmployeeAsync()
    //    {
    //        var res = new ResponseModel<List<Employee>>();
    //        try
    //        {
    //            var query = new GraphQLRequest
    //            {
    //                Query = @" 
    //                    { employees 
    //                        { name 
    //                          email  
    //                          certifications
    //                             { title }
    //                        }
    //                    }",
    //            };
    //            var response = await _graphQLClient.PostAsync(query);
    //            if (response.Errors != null && response.Errors.Any())
    //            {
    //                //throw new ApplicationException(response.Errors[0].Message);
    //                res.Errors.Add(new ErrorModel()
    //                {
    //                    Code = response.Errors[0].Message,
    //                    Message = response.Errors[0].Message
    //                });
    //            }
    //            var data = response.GetDataFieldAs<List<Employee>>("employees");
    //            res.Data = data;
                
    //        }
    //        catch (Exception e)
    //        {
    //            res.Errors.Add(new ErrorModel()
    //            {
    //                Code = e.Message,
    //                Message = e.InnerException.Message
    //            });
    //        }

    //        return res;
    //    }


    //    public async Task<ResponseModel<Employee>> GetEmployeeByIdAsync(int id)
    //    {
    //        var res = new ResponseModel<Employee>();
    //        try
    //        {
    //            var query = new GraphQLRequest
    //            {
    //                Query = @"   
    //                    query employeeQuery($employeeId: ID!)  
    //                    { employee(id: $employeeId)   
    //                        { id name email   
    //                        }  
    //                    }",
    //                Variables = new { employeeId = id }
    //            };
    //            var response = await _graphQLClient.PostAsync(query);
    //            if (response.Errors != null && response.Errors.Any())
    //            {
    //                //throw new ApplicationException(response.Errors[0].Message);
    //                res.Errors.Add(new ErrorModel()
    //                {
    //                    Code = response.Errors[0].Message,
    //                    Message = response.Errors[0].Message
    //                });
    //            }
    //            var data = response.GetDataFieldAs<Employee>("employee");
    //            res.Data = data;

    //        }
    //        catch (Exception e)
    //        {
    //            res.Errors.Add(new ErrorModel()
    //            {
    //                Code = e.Message,
    //                Message = e.InnerException.Message
    //            });
    //        }

    //        return res;
    //    }

    //    //todo
    //    public async Task<ResponseModel<List<Employee>>> SearchEmployeeAsync()
    //    {
    //        //test
    //        _httpClient.BaseAddress = new Uri(_configuration["GraphQlEndpoint"]);
    //        var response = await _httpClient.GetAsync(
    //            @"?query={employees 
    //                        { name 
    //                          email  
    //                          certifications
    //                             { title }
    //                        }
    //                    }");

    //        var stringResult = await response.Content.ReadAsStringAsync();
    //        return JsonConvert.DeserializeObject<ResponseModel<List<Employee>>>(stringResult);
    //    }
    //}
}
