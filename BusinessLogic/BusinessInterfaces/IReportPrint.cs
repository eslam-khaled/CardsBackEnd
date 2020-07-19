using DTOs.APIDtos;
using EPLHouse.Cards.DTOs.APIDtos;
using EPLHouse.Test.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public interface IReportPrint
    {
        bool CreateReportPrint();
        List<CardsPrintRequestsDTO> GetRequestsList();
        List<CardsPrintRequestsDTO> GetAllNewCards();
        List<Card> CreateRequestToPrint();
        bool CreatePrintFileSMT();
    }
}
