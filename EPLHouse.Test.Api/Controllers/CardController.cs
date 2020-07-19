using Microsoft.AspNetCore.Mvc;
using BusinessLogic;
using Microsoft.AspNetCore.Authorization;
using DTOs.APIDtos;
using DTOs.BusinessDTOs;
using System.Collections.Generic;
using System;
using DTOs.Enums;
using Microsoft.AspNetCore.Cors;
using System.IO;
using System.Net.Http;

namespace EPLHouse.Cards.Api.Controllers
{
    [Route("api/card")]
    [ApiController]
    //[Authorize]
    [AllowAnonymous]
    public class CardController : ControllerBase
    {
        private readonly ICardBusiness _cardBusiness;
        public CardController(ICardBusiness cardBusiness)
        {
            _cardBusiness = cardBusiness;
        }
     
        [HttpPost]
        public void AddNewCard(NewCardDtoAPI newCardDtoAPI)
        {
            NewCardDtoBusiness newCardDtoBusiness = new NewCardDtoBusiness();
            if (ModelState.IsValid)
            {
                
                newCardDtoBusiness.firstNameOnCard = newCardDtoAPI.firstNameOnCard;
                newCardDtoBusiness.secondNameOnCard = newCardDtoAPI.secondNameOnCard;
                newCardDtoBusiness.familyNameOnCard = newCardDtoAPI.familyNameOnCard;
                newCardDtoBusiness.majorOrSub = (CardMajorOrSubEnum)Enum.Parse(typeof(CardMajorOrSubEnum), newCardDtoAPI.majorOrSub);
                newCardDtoBusiness.cardLevel = (CardLevelEnum)Enum.Parse(typeof(CardLevelEnum), newCardDtoAPI.cardLevel.ToString());
                newCardDtoBusiness.accountNumber = newCardDtoAPI.AccountNumber;
                newCardDtoBusiness.comment = newCardDtoAPI.Comment;
                newCardDtoBusiness.deliveryBranch = newCardDtoAPI.DeliveryBranch;


                _cardBusiness.CreateNewCard(newCardDtoBusiness);
            }
            else
            {
                throw new Exception("Can't create new card!");
            }
        }

        
        [Route("ReplaceCardByAccountNumber")]
        [HttpPut]
        public void ReplaceCardByAccountNumber(string accountNumber)
        {

            ClientDtoBusiness clientDtoBusiness = new ClientDtoBusiness();
            clientDtoBusiness.accountNumber = accountNumber;

            if (ModelState.IsValid)
            {
                _cardBusiness.ReplaceCardByAccountNumber(clientDtoBusiness);
            }
            else
            {
                throw new Exception("Can't Replace the card!");
            }
        }

        
        [Route("ReplaceCardByCardNumber")]
        [HttpPut]
        public void ReplaceCardByCardNumber(CardDtoBusiness cardDtoBusiness)
        {
            if (ModelState.IsValid)
            {
                 
                _cardBusiness.ReplaceCardByCardNumber(cardDtoBusiness);
            }
            else
            {
                throw new Exception("Can't Replace the card!");
            }
        }

        
        [Route("ReNewByAccountNumber")]
        [HttpPut]
        public void ReNewByAccountNumber(string accountNumber)
        {
            if (ModelState.IsValid)
            {
                _cardBusiness.ReNewByAccountNumber(accountNumber);
            }
            else
            {
                throw new Exception("Can't Renew the card!");
            }

        }

        
        [Route("ReNewByCardNumber")]
        [HttpPut]
        public void ReNewByCardNumber(CardDtoBusiness cardDtoBusiness)
        {
            if (ModelState.IsValid)
            {
                _cardBusiness.ReNewByCardNumber(cardDtoBusiness);
            }
            else
            {
                throw new Exception("Can't Renew the card!");
            }

        }

        
        [Route("ReGenerateByAccountNumber")]
        [HttpPut]
        public void ReGenerateByAccountNumber(string accountNumber)
        {
            if (ModelState.IsValid)
            {
                _cardBusiness.ReGenerateByAccountNumber(accountNumber);
            }
            else
            {
                throw new Exception("Can't ReGenerate the card!");
            }
        }

        
        [Route("ReGenerateByCardNumber")]
        [HttpPut]
        public void ReGenerateByCardNumber(CardDtoBusiness cardDtoBusiness)
        {
            if (ModelState.IsValid)
            {
                _cardBusiness.ReGenerateByCardNumber(cardDtoBusiness);
            }
            else
            {
                throw new Exception("Can't ReGenerate the card!");
            }
        }

        
        [Route("GetCardsByAccountNumber")]
        [HttpGet]
        public List<CardDtoBusiness> GetCardsByAccountNumber(string accountNumber)
        {
            List<CardDtoBusiness> cardDtoBusiness = new List<CardDtoBusiness>();
            var cardsList = _cardBusiness.GetCardsByAccountNumber(accountNumber);
            cardDtoBusiness = cardsList;
            return cardDtoBusiness;

        }

       
        [Route("GetCardsByCardNumber")]
        [HttpGet]
        public List<CardDtoBusiness> GetCardsByCardNumber(string cardNumber)
        {
            List<CardDtoBusiness> cardDtoBusiness = new List<CardDtoBusiness>();
            var cardList = _cardBusiness.GetCardsByCardNumber(cardNumber);
            cardDtoBusiness = cardList;
            return cardDtoBusiness;
        }

        
        [Route("GetCardsByIdentityNumber")]
        [HttpGet]
        public List<CardDtoBusiness> GetCardsByIdentityNumber(string identityNumber)
        {
            List<CardDtoBusiness> CardDtoBusiness = new List<CardDtoBusiness>();
            var CardsList = _cardBusiness.GetCardsByIdentityNumber(identityNumber);
            CardDtoBusiness = CardsList;
            return CardDtoBusiness;
        }
        
        [Route("GetCardDetailsById")]
        [HttpGet]
        public CardDtoBusiness GetCardDetailsById(int id)
        {
            return _cardBusiness.GetCardDetailsById(id);
        }

       
        [Route("CloseCard")]
        [HttpPut]
        public CardDtoBusiness CloseCard(CardDtoBusiness cardDtoBusiness)
        {
            _cardBusiness.CloseCard(cardDtoBusiness);
            return cardDtoBusiness;
        }

        
        [Route("CancelCard")]
        [HttpPut]
        public CardDtoBusiness CancelCard(CardDtoBusiness cardDtoBusiness)
        {
            _cardBusiness.CancelCard(cardDtoBusiness);
            return cardDtoBusiness;
        }
        
        [Route("GetNewCardExtraInfo")]
        [HttpGet]
        public List<CardLevelDtoBusiness> GetNewCardExtraInfo()
        {
          return _cardBusiness.GetNewCardExtraInfo();
            
        }
    }
}