using Azure.Core.Serialization;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Store.BusinessMS.Users.Application.Dtos;
using Store.BusinessMS.Users.Application.Query;
using System.Net;
using Store.BusinessMS.Users.Application.Wrappers;
using Store.BusinessMS.Users.Application.Query.GetUsers;
using System.Web;
using Store.BusinessMS.Users.Application.Command.CreateOtp.Response;
using Store.BusinessMS.Users.Application.Command.CreateOtp;
using System.Collections.Specialized;
using Store.BusinessMS.Users.Application.Core;
using Store.BusinessMS.Users.Application.Command.RegisterUser.Request;
using Store.BusinessMS.Users.Application.Commands.RegisterUser;
using Store.BusinessMS.Users.Application.Command.UpdateUser;
using Store.BusinessMS.Users.Application.Command.ChangePassword;
using System.Text.Json;

namespace Store.BusinessMS.Users.Functions;

public class UsersFunction
{
    private readonly ILogger _logger;
    private readonly IMediator _mediator;
    private readonly NewtonsoftJsonObjectSerializer _serializer;

    public UsersFunction(ILoggerFactory loggerFactory, IMediator mediator, NewtonsoftJsonObjectSerializer serializer)
    {
        _serializer = serializer;
        _mediator = mediator;
        _logger = loggerFactory.CreateLogger<UsersFunction>();
    }

    [Function("RegisterUser")]
    public async Task<HttpResponseData> RegisterUser(
    [HttpTrigger(AuthorizationLevel.Function, "post", Route = "users/register")] HttpRequestData httpRequest)
    {
        var requestBody = await httpRequest.ReadAsStringAsync();
        var request = JsonConvert.DeserializeObject<RegisterUserRequest>(requestBody);
        var command = new RegisterUser.Command(request);
        var result = await _mediator.Send(command);
        var response = httpRequest.CreateResponse(HttpStatusCode.Created);
        await response.WriteAsJsonAsync(result);
        return response;
    }

    [Function("UpdateUser")]
    public async Task<HttpResponseData> UpdateUser(
    [HttpTrigger(AuthorizationLevel.Function, "put", Route = "users/update")] HttpRequestData req)
    {
        var requestBody = await req.ReadAsStringAsync();
        var command = JsonConvert.DeserializeObject<UpdateUser.Command>(requestBody);
        var result = await _mediator.Send(command);
        var response = req.CreateResponse(result.Succeded ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
        await response.WriteAsJsonAsync(result);
        return response;
    }

    [Function("ChangePassword")]
    public async Task<HttpResponseData> Run(
    [HttpTrigger(AuthorizationLevel.Function, "post", Route = "users/changePassword")] HttpRequestData req)
    {
        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var command = System.Text.Json.JsonSerializer.Deserialize<ChangePassword.Command>(requestBody, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (command == null)
        {
            var badResponse = req.CreateResponse(HttpStatusCode.BadRequest);
            await badResponse.WriteStringAsync("Invalid request payload.");
            return badResponse;
        }

        var result = await _mediator.Send(command);
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(result);
        return response;
    }

    [Function("GetUserById")]
    public async Task<HttpResponseData> GetUserById(
    [HttpTrigger(AuthorizationLevel.Function, "get", Route = "users/{id}")] HttpRequestData httpRequest, string id)
    {
        var httpResponse = httpRequest.CreateResponse(HttpStatusCode.OK);
        var responseData = Response<GetByIdDto>.Success(await _mediator.Send(new GetUserById.Query(id)));
        var jsonResponse = JsonConvert.SerializeObject(responseData, Formatting.Indented);
        await httpResponse.WriteStringAsync(jsonResponse);
        return httpResponse;
    }

    [Function("GetAllUsers")]
    public async Task<HttpResponseData> GetUsers(
    [HttpTrigger(AuthorizationLevel.Function, "get", Route = "users")] HttpRequestData httpRequest)
    {
        NameValueCollection query = HttpUtility.ParseQueryString(httpRequest.Url.Query);

        int pageNumber = !string.IsNullOrEmpty(query["pageNumber"]) ? int.Parse(query["pageNumber"]!) : 1;
        int pageSize = !string.IsNullOrEmpty(query["pageSize"]) ? int.Parse(query["pageSize"]!) : 15;

        var httpResponse = httpRequest.CreateResponse(HttpStatusCode.OK);

        var result = await _mediator.Send(new GetUsers.Query
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        });

        var responseData = Response<PagedList<UserDto>>.Success(result);
        var jsonResponse = JsonConvert.SerializeObject(responseData, Formatting.Indented);
        await httpResponse.WriteStringAsync(jsonResponse);
        return httpResponse;
    }

    [Function("CreateOtp")]
    public async Task<HttpResponseData> CreateOtp(
     [HttpTrigger(AuthorizationLevel.Function, "post", Route = "users/createOtp/")] HttpRequestData httpRequest)
    {
        var requestData = await new StreamReader(httpRequest.Body).ReadToEndAsync();
        var dto = JsonConvert.DeserializeObject<OtpDto>(requestData);
        var httpResponse = httpRequest.CreateResponse(HttpStatusCode.OK);
        var responseData = Response<ResponseOtp>.Success(await _mediator.Send(new CreateOtp.Command(dto)));
        var jsonResponse = JsonConvert.SerializeObject(responseData, Formatting.Indented);
        await httpResponse.WriteStringAsync(jsonResponse);
        return httpResponse;
    }
}