using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.BusinessInterfaces;
using DTOs.BusinessDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EPLHouse.Cards.Controllers
{
    [Route("api/client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientBusiness _clientBusiness;
        public ClientController(IClientBusiness clientBusiness)
        {
            _clientBusiness = clientBusiness;
        }

        [Route("GetClientDetailsByAccountNumber")]
        [HttpGet]
        public ClientDtoBusiness GetClientDetailsByAccountNumber(string accountNumber)
        {

            var result = HttpContext.Request.Headers.Keys;

            ClientDtoBusiness clientDtoBusiness = new ClientDtoBusiness();
            var ClientDetails = _clientBusiness.GetClientDetailsByAccountNumber(accountNumber);
            clientDtoBusiness = ClientDetails;
            return clientDtoBusiness;
        }


        [Route("GetClientDetailsByCardNumber")]
        [HttpGet]
        public ClientDtoBusiness GetClientDetailsByCardNumber(string cardNumber)
        {
            ClientDtoBusiness clientDtoBusiness = new ClientDtoBusiness();
            var ClientDetails = _clientBusiness.GetClientDetailsByCardNumber(cardNumber);
            clientDtoBusiness = ClientDetails;
            return clientDtoBusiness;
        }


        [Route("GetClientDetailsByIdentityNumber")]
        [HttpGet]
        public ClientDtoBusiness GetClientDetailsByIdentityNumber(string identityNumber)
        {
            return _clientBusiness.GetClientDetailsIdentityNumber(identityNumber);
        }

        [Route("EditClientAddress")]
        [HttpPut]
        public ClientDtoBusiness EditClientAddress(ClientDtoBusiness clientDtoBusiness)
        {
            _clientBusiness.EditClientAddress(clientDtoBusiness);
            return clientDtoBusiness;
        }


    }
}