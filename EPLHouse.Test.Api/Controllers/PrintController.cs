using BusinessLogic;
using DTOs.APIDtos;
using EPLHouse.Cards.DTOs.APIDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EPLHouse.Cards.Controllers
{
    [Route("api/printCard")]
    [ApiController]
    [AllowAnonymous]
    public class PrintController : ControllerBase
    {
        private readonly IReportPrint _reportPrintBusiness;
        public PrintController(IReportPrint reportPrint)
        {
            _reportPrintBusiness = reportPrint;
        }

        [HttpGet("CreatePrintFileEBE")]
        public void CreatePrintFile()
        {
            _reportPrintBusiness.CreateReportPrint();
        }

        [HttpGet("CreatePrintFileSMT")]
        public void CreatePrintFileSMT()
        {
            _reportPrintBusiness.CreatePrintFileSMT();
        }

        [HttpGet("GetPrintRequestsList")]
        public List<CardsPrintRequestsDTO> GetRequestsList()
        {
            List<CardsPrintRequestsDTO> cardsPrintRequests = _reportPrintBusiness.GetRequestsList();
            return cardsPrintRequests;
        }

        [HttpGet("GetAllNewCards")]
        public List<CardsPrintRequestsDTO> GetAllNewCards()
        {
            List<CardsPrintRequestsDTO> cardsList = _reportPrintBusiness.GetAllNewCards();
            return cardsList;
        }

        [HttpGet("CreatePrintRequest")]
        public void CreatePrintRequest()
        {
            _reportPrintBusiness.CreateRequestToPrint();
        }
    }
}