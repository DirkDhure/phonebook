using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PhoneBook.Abstractions;
using PhoneBook.Abstractions.Commands;
using PhoneBook.Abstractions.Model;
using PhoneBook.Abstractions.Services;
using PhoneBook.API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PhoneBook.API.Controllers
{
    [Route("api/phone-books")]
    [ApiController]
    public class PhoneBooksController : ControllerBase
    {

        ILogger<PhoneBooksController> _logger;
        private readonly LoggingContext loggingContext;
        private readonly IPhoneBookApplication _phoneBookApplication;


        public PhoneBooksController(IPhoneBookApplication phoneBookApplication, ILogger<PhoneBooksController> logger)
        {
            _logger = logger;
            _phoneBookApplication = phoneBookApplication;
            loggingContext = new LoggingContext("localhost", "Phone Book Manager API", "PhoneBooksController", "", 200);

        }


        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(CommandResult<Abstractions.Model.PhoneBook>), StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreatePhoneBook([FromBody] PhoneBookDetail phoneBook)
        {

            loggingContext.ActionName = MethodBase.GetCurrentMethod().Name;
            var data = JsonConvert.SerializeObject(phoneBook);
            _logger.LogInformation("Server Name: {0} Api Name : {1} Contoller Name : {2}, Action Name :{3} Status Code :{4} \n Data :{5}", loggingContext.ServerName, loggingContext.APIName, loggingContext.ControllerName, loggingContext.ActionName, loggingContext.StatusCode, data);
            RequestContext context = SecurityContextHelper.GetCurrentRequestContext(Request.HttpContext.User);

            phoneBook.OwnerEmail = context.UserEmail;
            var command = new CreatePhoneBook
            {
                CommandData = phoneBook,
                UserId = context.UserId,
                UserEmail = context.UserEmail,
                SubscriptionId = context.SubscriptionId

            };
            var commandResult = await _phoneBookApplication.CreatePhoneBook(command, context);
            return Ok(commandResult);

        }


        [HttpPost]
        [Route("{phoneBookId}")]
        [ProducesResponseType(typeof(CommandResult<Abstractions.Model.PhoneBookEntry>), StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreatePhoneBookEntry(Guid phoneBookId, [FromBody] PhoneBookEntry phoneBookEntry)
        {

            loggingContext.ActionName = MethodBase.GetCurrentMethod().Name;
            var data = JsonConvert.SerializeObject(phoneBookEntry);
            _logger.LogInformation("Server Name: {0} Api Name : {1} Contoller Name : {2}, Action Name :{3} Status Code :{4} \n Data :{5}", loggingContext.ServerName, loggingContext.APIName, loggingContext.ControllerName, loggingContext.ActionName, loggingContext.StatusCode, data);
            RequestContext context = SecurityContextHelper.GetCurrentRequestContext(Request.HttpContext.User);

            phoneBookEntry.PhoneBookId = phoneBookId;
            var command = new CreatePhoneBookEntry
            {
                CommandData = phoneBookEntry,
                UserId = context.UserId,
                UserEmail = context.UserEmail,
                SubscriptionId = context.SubscriptionId

            };
            var commandResult = await _phoneBookApplication.CreatePhoneBookEntry(command, context);
            return Ok(commandResult);

        }


        [HttpPut]
        [Route("{phoneBookId}")]
        [ProducesResponseType(typeof(CommandResult<Abstractions.Model.PhoneBookEntry>), StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdatePhoneBookEntry(Guid phoneBookId, [FromBody] PhoneBookEntry phoneBookEntry)
        {

            loggingContext.ActionName = MethodBase.GetCurrentMethod().Name;
            var data = JsonConvert.SerializeObject(phoneBookEntry);
            _logger.LogInformation("Server Name: {0} Api Name : {1} Contoller Name : {2}, Action Name :{3} Status Code :{4} \n Data :{5}", loggingContext.ServerName, loggingContext.APIName, loggingContext.ControllerName, loggingContext.ActionName, loggingContext.StatusCode, data);
            RequestContext context = SecurityContextHelper.GetCurrentRequestContext(Request.HttpContext.User);

            phoneBookEntry.PhoneBookId = phoneBookId;
            var command = new UpdatePhoneBookEntry
            {
                CommandData = phoneBookEntry,
                UserId = context.UserId,
                UserEmail = context.UserEmail,
                SubscriptionId = context.SubscriptionId

            };
            var commandResult = await _phoneBookApplication.UpdatePhoneBookEntry(command, context);
            return Ok(commandResult);

        }


        [HttpGet]
        [Route("{emailAddress}")]
        [ProducesResponseType(typeof(Abstractions.Model.PhoneBook), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetPhoneBook(string emailAddress)
        {

            loggingContext.ActionName = MethodBase.GetCurrentMethod().Name;
            var data = JsonConvert.SerializeObject(emailAddress);
            _logger.LogInformation("Server Name: {0} Api Name : {1} Contoller Name : {2}, Action Name :{3} Status Code :{4} \n Data :{5}", loggingContext.ServerName, loggingContext.APIName, loggingContext.ControllerName, loggingContext.ActionName, loggingContext.StatusCode, data);
            RequestContext context = SecurityContextHelper.GetCurrentRequestContext(Request.HttpContext.User);


            return Ok(new PhoneBook.Abstractions.Model.PhoneBook());

        }


        [HttpGet]
        [Route("{phoneBookId}/entries/{entryId}")]
        [ProducesResponseType(typeof(Abstractions.Model.PhoneBookContact), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetPhoneBookEntry(Guid phoneBookId, Guid entryId)
        {
            loggingContext.ActionName = MethodBase.GetCurrentMethod().Name;
            var data = JsonConvert.SerializeObject(phoneBookId);
            _logger.LogInformation("Server Name: {0} Api Name : {1} Contoller Name : {2}, Action Name :{3} Status Code :{4} \n Data :{5}", loggingContext.ServerName, loggingContext.APIName, loggingContext.ControllerName, loggingContext.ActionName, loggingContext.StatusCode, data);
            RequestContext context = SecurityContextHelper.GetCurrentRequestContext(Request.HttpContext.User);

            return Ok(new PhoneBook.Abstractions.Model.PhoneBook());

        }



        [HttpDelete]
        [Route("{phoneBookId}/entries/{entryId}")]
        [ProducesResponseType(typeof(Abstractions.Model.PhoneBook), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeletePhoneBookEntry(Guid phoneBookId, Guid entryId)
        {

            loggingContext.ActionName = MethodBase.GetCurrentMethod().Name;
            var data = JsonConvert.SerializeObject(phoneBookId);
            _logger.LogInformation("Server Name: {0} Api Name : {1} Contoller Name : {2}, Action Name :{3} Status Code :{4} \n Data :{5}", loggingContext.ServerName, loggingContext.APIName, loggingContext.ControllerName, loggingContext.ActionName, loggingContext.StatusCode, data);
            RequestContext context = SecurityContextHelper.GetCurrentRequestContext(Request.HttpContext.User);

            return Ok(new PhoneBook.Abstractions.Model.PhoneBook());

        }

    }
}
