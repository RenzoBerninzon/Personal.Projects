using Azure.Core.Serialization;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Store.BusinessMS.Users.Application.Dtos;
using Store.BusinessMS.Users.Application.Query.GetById;
using System.Net;
using Store.BusinessMS.Users.Application.Wrappers;
using Store.BusinessMS.Users.Application.Query.GetUsers;
using System.Web;
using Store.BusinessMS.Users.Application.Command.CreateOtp.Response;
using Store.BusinessMS.Users.Application.Command.CreateOtp;
using System.Collections.Specialized;
using Store.BusinessMS.Users.Application.Core;

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

    [Function("GetUserById")]
    public async Task<HttpResponseData> GetById(
    [HttpTrigger(AuthorizationLevel.Function, "get", Route = "users/{id}")] HttpRequestData httpRequest, string id)
    {
        var httpResponse = httpRequest.CreateResponse(HttpStatusCode.OK);
        var responseData = Response<GetByIdDto>.Success(await _mediator.Send(new GetById.Query(id)));
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