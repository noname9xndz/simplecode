using System;
using GraphQL.Client;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Types;
using GraphQLGraphTypeFirstNestedTable.GraphQL.Clients;
using GraphQLGraphTypeFirstNestedTable.Models;
using GraphQL.Client.Serializer.Newtonsoft;
using GraphQLGraphTypeFirstNestedTable.GraphQL;

namespace GraphQLGraphTypeFirstNestedTable.Controllers
{
    [Route("Employee")]
    public class EmployeeController : Controller
    {
        //private readonly ISchema _schema;
        //private readonly IDocumentExecuter _executer;
        //private readonly IEmployeeGraphqlClient _employeeGraphqlClient;
        public EmployeeController(
            //ISchema schema,
            //IDocumentExecuter executer,
       //    IEmployeeGraphqlClient employeeGraphqlClient
            )
        {
            //_schema = schema;
            //_executer = executer;
           // _employeeGraphqlClient = employeeGraphqlClient;
        }
        [HttpGet]
        public async Task<List<Employee>> Get()
        {
            try
            {
                using (var graphQLClient = new GraphQLHttpClient("http://localhost:64350/graphql", new NewtonsoftJsonSerializer()))
                {
                    var query = new GraphQLRequest
                    {
                        Query = @" 
                        { employees 
                            { name 
                              email  
                              certifications
                                 { title }
                            }
                        }",
                    };
                    var response = await graphQLClient.SendQueryAsync<List<Employee>>(query);
                    return response.Data;
                    //return response.GetDataFieldAs<List<Employee>>("employees");
                }
            }
            catch (Exception e)
            {
                return new List<Employee>();
            }
            
        }

        [HttpGet("{id}")]
        public async Task<Employee> Get(int id)
        {
            using (var graphQLClient = new GraphQLHttpClient("http://localhost:64350/graphql", new NewtonsoftJsonSerializer()))
            {
                var query = new GraphQLRequest
                {
                    Query = @"   
                        query employeeQuery($employeeId: ID!)  
                        { employee(id: $employeeId)   
                            { id name email   
                            }  
                        }",
                    Variables = new { employeeId = id }
                };
                var response = await graphQLClient.SendQueryAsync<Employee>(query);
                return response.Data;
                //var response = await graphQLClient.PostAsync(query);
                //return response.GetDataFieldAs<Employee>("employee");
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> Post([FromBody]
        //    GraphQLQueryDTO query)
        //{
        //    var result = await _executer.ExecuteAsync(option =>
        //    {
        //        option.Schema = _schema;
        //        option.Query = query.Query;
        //        option.OperationName = query.OperationName;
        //      //  option.Inputs = query.Variables?.ToInputs();

        //    });
        //    if (result.Errors?.Count > 0)
        //    {
        //        return BadRequest();
        //    }
        //    return Ok(result.Data);
        //}
    }


}
