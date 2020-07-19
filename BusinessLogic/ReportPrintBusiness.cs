using DTOs.APIDtos;
using EPLHouse.Cards.DataAccess.Models;
using EPLHouse.Cards.DTOs.APIDtos;
using EPLHouse.Test.DataAccess.Models;
using EPLHouse.Test.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class ReportPrintBusiness : IReportPrint
    {
        private AppDBContext _context;
        private readonly IConfiguration _configuration;
        private IBaseRepository<PrintRequests> _printRequrstbaseRepository;
        private IBaseRepository<Card> _cardbaseRepository;

        public ReportPrintBusiness(AppDBContext appDBContext, IBaseRepository<PrintRequests> printRequrstbaseRepository,
            IBaseRepository<Card> cardbaseRepository, IConfiguration configuration)
        {
            _context = appDBContext;
            _printRequrstbaseRepository = printRequrstbaseRepository;
            _cardbaseRepository = cardbaseRepository;
            _configuration = configuration;
        }

        public List<Card> CreateRequestToPrint()
        {
            List<Card> requestedCards = _cardbaseRepository.GetWhere(x => x.IsRequestedToPrint == false && x.IsPrinted == false).ToList();

            PrintRequests printRequests = new PrintRequests()
            {
                RequestDate = DateTime.Now
            };
            var requesrDetails = _printRequrstbaseRepository.AddNew(printRequests);


            foreach (var card in requestedCards)
            {
                card.printRequestsID = requesrDetails.Id;
                card.IsRequestedToPrint = true;
                _cardbaseRepository.Update(card);
            }

            return requestedCards;
        }

        private List<Card> GetNotPrintedCards()
        {
            try
            {
                List<Card> cardList = _context.Cards.Where(x => x.IsRequestedToPrint == true && x.IsPrinted == false).ToList();
                if (cardList == null)
                {
                    throw new Exception("Empty List!");
                }
                return cardList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CreatePrintFileSMT()

        {
            string printFilePath = _configuration["PhysicalPrintPath:FilePath"];
            try
            {
                string Nowdate = DateTime.Now.ToString();
                Nowdate = Nowdate.Replace('/', '_');
                Nowdate = Nowdate.Replace(':', '_');
                TextWriter textWriter = new StreamWriter(printFilePath + Nowdate + ".txt", true);

                string printText = "";

                List<Card> PrintRequestsList = GetNotPrintedCards();
                foreach (var card in PrintRequestsList)
                {
                    printText = "";
                    textWriter.WriteLine(printText);
                    card.IsPrinted = true;
                    _cardbaseRepository.Update(card);
                }
                textWriter.Close();
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }


        public bool CreateReportPrint()
        {
            string printFilePath = _configuration["PhysicalPrintPath:FilePath"];
            try
            {
                string Nowdate = DateTime.Now.ToString();
                Nowdate = Nowdate.Replace('/', '_');
                Nowdate = Nowdate.Replace(':', '_');
                TextWriter textWriter = new StreamWriter(printFilePath + Nowdate + ".txt", true);

                string printText = "";

                List<Card> PrintRequestsList = GetNotPrintedCards();
                foreach (var card in PrintRequestsList)
                {
                    if (card.CardsLevel_Id == 10 || card.CardsLevel_Id == 11 || card.CardsLevel_Id == 12)
                    {
                        string NameOnCard = card.firstNameOnCard + " " + card.secondNameOnCard + " " + card.familyNameOnCard;
                        string fullNameOnCard = NameOnCard.PadRight(26);
                        string TrackOneTotal = card.CardNumber + "^" + fullNameOnCard + "^" + card.CardExpiryDate.ToString("yymm").Substring(0, 4)
                            + "2211" + "8" + "9210" + "000000000" + card.CVV + "000000";
                        string trackOne = TrackOneTotal.PadRight(75);
                        string TrackTwoTotal = card.CardNumber
                            + card.CardExpiryDate.ToString("yymm").Substring(0, 4) + "2211" + "8" + "9210" + card.CVV + "00000";
                        string trackTwo = TrackTwoTotal.PadRight(37);
                        string EncryptedPIN = card.PIN.ToString();
                        string PIN = EncryptedPIN.PadRight(16);

                        printText = card.CardNumber + card.StartDate.ToString("yyMMdd").Substring(0, 6)
                            + card.CardExpiryDate.ToString("yyMMdd").Substring(0, 6)
                            + fullNameOnCard + "%B" + trackOne + "?" + ";" + trackTwo + "?"
                            + card.CVV + card.ICVV + PIN;
                    }
                    else if (card.CardsLevel_Id == 7 || card.CardsLevel_Id == 8 || card.CardsLevel_Id == 9)
                    {
                        string NameOnCard = card.firstNameOnCard + " " + card.secondNameOnCard + " " + card.familyNameOnCard;
                        string fullNameOnCard = NameOnCard.PadRight(26);
                        string TrackOneTotal = card.CardNumber + "^" + fullNameOnCard + "^" + card.CardExpiryDate.ToString("yymm").Substring(0, 4)
                            + "2211" + "8" + "9210" + "000000000" + card.CVV + "000000";
                        string trackOne = TrackOneTotal.PadRight(75);
                        string TrackTwoTotal = card.CardNumber
                            + card.CardExpiryDate.ToString("yymm").Substring(0, 4) + "2211" + "8" + "9210" + card.CVV + "00000";
                        string trackTwo = TrackTwoTotal.PadRight(37);
                        string EncryptedPIN = card.PIN.ToString();
                        string PIN = EncryptedPIN.PadRight(16);

                        printText = card.CardNumber + "00" + card.StartDate.ToString("yyMMdd").Substring(0, 6)
                            + card.CardExpiryDate.ToString("yyMMdd").Substring(0, 6)
                            + fullNameOnCard + "%B" + trackOne + "?" + ";" + trackTwo + "?"
                            + card.CVV + "2211" + card.ICVV + PIN;
                    }

                    textWriter.WriteLine(printText);
                    card.IsPrinted = true;
                    _cardbaseRepository.Update(card);
                }

                textWriter.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public List<CardsPrintRequestsDTO> GetRequestsList()
        {
            List<CardsPrintRequestsDTO> cardsPrintRequests = new List<CardsPrintRequestsDTO>();
            try
            {
                List<Card> cardsRequestsList = _cardbaseRepository.GetWhere(x => x.IsRequestedToPrint == true && x.IsPrinted == false).Include(y => y.printRequests).ToList();

                foreach (var Request in cardsRequestsList)
                {
                    cardsPrintRequests.Add(new CardsPrintRequestsDTO()
                    {
                        CardNumber = Request.CardNumber,
                        firstNameOnCard = Request.firstNameOnCard,
                        secondNameOnCard = Request.secondNameOnCard,
                        familyNameOnCard = Request.familyNameOnCard,
                        StartDate = Request.StartDate.ToString("dd/MM/yyyy HH:mm:ss"),
                        CardExpiryDate = Request.CardExpiryDate.ToString("dd/MM/yyyy HH:mm:ss")
                    });
                }

                return cardsPrintRequests;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<CardsPrintRequestsDTO> GetAllNewCards()
        {
            try
            {
                List<Card> cardList = _cardbaseRepository.GetWhere(x => x.IsRequestedToPrint == false && x.IsPrinted == false).Include(y => y.printRequests).ToList();


                List<CardsPrintRequestsDTO> cardsListDto = new List<CardsPrintRequestsDTO>();
                foreach (var card in cardList)
                {
                    cardsListDto.Add(new CardsPrintRequestsDTO
                    {
                        CardNumber = card.CardNumber,
                        firstNameOnCard = card.firstNameOnCard,
                        secondNameOnCard = card.secondNameOnCard,
                        familyNameOnCard = card.familyNameOnCard,
                        StartDate = card.StartDate.ToString("dd/MM/yyyy HH:mm:ss"),
                        CardExpiryDate = card.CardExpiryDate.ToString("dd/MM/yyyy HH:mm:ss")
                    });
                }
                return cardsListDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
