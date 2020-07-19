using DTOs.BusinessDTOs;
using EPLHouse.Test.DataAccess.Models;
using EPLHouse.Test.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public interface ICardBusiness
    {
        /// <summary>
        /// Takes First name, Last name, account number then create new card
        /// </summary>
        /// <param name="businessClientDto"><see cref="ClientDtoBusiness"/> is an object from ClientDtoBusiness to cary data belongs to client in the business layer</param>
        NewCardDtoBusiness CreateNewCard(NewCardDtoBusiness newCardDtoBusiness);

        /// <summary>
        /// takes account number to regenerate CCV and PIN code
        /// </summary>
        /// <param name="clientDtoBusiness"><see cref="clientDtoBusiness"</see>account number is a 13 digits - first 4 represents the branch, next 6 are the mean numbers, last 3 are the currency type</param>
        ClientDtoBusiness ReplaceCardByAccountNumber(ClientDtoBusiness clientDtoBusiness);

        /// <summary>
        /// takes card number to regenerate CCV and PIN code
        /// </summary>
        /// <param name="cardNumber"><see cref="cardNumber">card number is 16 digits on the back of the card</see></param>
        CardDtoBusiness ReplaceCardByCardNumber(CardDtoBusiness cardDtoBusiness);

        /// <summary>
        /// Replace card by identity number, the Replace method regenerates the CCV & PIN codes then replace them with the old ones
        /// </summary>
        /// <param name="IdentityNumber"><see cref="IdentityNumber"></see></param>
        /// <returns></returns>
        CardDtoBusiness ReplaceCardByIdentity(string IdentityNumber);

        /// <summary>
        ///  Replace card by account number, the Replace method regenerates the CCV & PIN codes then replace them with the old ones
        /// </summary>
        /// <param name="accountNumber"><see cref="accountNumber">account number is a 13 digits - first 4 represents the branch, next 6 are the mean numbers, last 3 are the currency type</see></param>
        ClientDtoBusiness ReNewByAccountNumber(string accountNumber);

        /// <summary>
        /// Renew card by card number, the renew method regenerates the CCV code and replace it with the old one
        /// </summary>
        /// <param name="cardNumber"><see cref="cardNumber">card number is 16 digits on the back of the card</see></param>
        CardDtoBusiness ReNewByCardNumber(CardDtoBusiness cardDtoBusiness);

        /// <summary>
        /// Renew card by identity number, the renew method regenerates the CCV code and replace it with the old one
        /// </summary>
        /// <param name="IdentityNumber"><see cref="IdentityNumber"></see></param>
        /// <returns></returns>
        CardDtoBusiness ReNewByIdentity(string IdentityNumber);

        /// <summary>
        /// takes account number to regenerate PIN code
        /// </summary>
        /// <param name="accountNumber"><see cref="accountNumber"></see>account number is a 13 digits - first 4 represents the branch, next 6 are the mean numbers, last 3 are the currency type</param>
        ClientDtoBusiness ReGenerateByAccountNumber(string accountNumber);

        /// <summary>
        /// Regenerate card by identity number, the renew method regenerates the PIN code and replace it with the old one
        /// </summary>
        /// <param name="IdentityNumber"><see cref="IdentityNumber"></see></param>
        /// <returns></returns>
        CardDtoBusiness ReGenerateByIdentity(string IdentityNumber);

        /// <summary>
        /// takes card number to regenerate PIN code
        /// </summary>
        /// <param name="cardNumber"><see cref="cardNumber"></see>card number is 16 digits on the back of the card</param>
        CardDtoBusiness ReGenerateByCardNumber(CardDtoBusiness cardDtoBusiness);


        /// <summary>
        /// Get all cards for a specific ACCOUNT number
        /// </summary>
        /// <param name="accountNumber"><see cref="accountNumber"></see></param>
        /// <returns></returns>
        List<CardDtoBusiness> GetCardsByAccountNumber(string accountNumber);

        /// <summary>
        /// Get all cards for a specific CARD number
        /// </summary>
        /// <param name="cardNumber"><see cref="cardNumber"></see></param>
        /// <returns></returns>
        List<CardDtoBusiness> GetCardsByCardNumber(string cardNumber);

        /// <summary>
        /// Get all cards for a specific IDENTITY number
        /// </summary>
        /// <param name="identityNumber"><see cref="identityNumber"></see></param>
        /// <returns></returns>
        List<CardDtoBusiness> GetCardsByIdentityNumber(string identityNumber);

        /// <summary>
        /// get one client details by account number
        /// </summary>
        /// <param name="accountNumber"><see cref="accountNumber"></see></param>
        /// <returns></returns>
        /// <summary>
        /// Sends request to SMT Gateway to start printing the card
        /// </summary>
        /// <param name="card"><see cref="card"/></param>
        /// <returns></returns>
        bool SendRequestToSMT(Card card);

        /// <summary>
        /// Discount the charge from the account balance after finishing a successful card creation
        /// </summary>
        /// <param name="account"><see cref="account"/></param>
        /// <returns></returns>
        Account ChargeOnIssueNewCard(NewCardDtoBusiness newCardDtoBusiness);

        /// <summary>
        /// Check if this account is subscribed in 084 program, if yes continue, if no throw exception and stop process
        /// </summary>
        /// <param name="account"><see cref="account"/></param>
        /// <returns></returns>
        bool CheckIfSubscribedOn084Program(Account account);

        /// <summary>
        /// Get one card details by id
        /// </summary>
        /// <param name="id"><see cref="id"/></param>
        /// <returns></returns>
        CardDtoBusiness GetCardDetailsById(int id);

        /// <summary>
        /// Changes card status to Closed by account number recieved in an object
        /// </summary>
        /// <param name="cardDtoBusiness"><see cref="cardDtoBusiness"/></param>
        /// <returns></returns>
        CardDtoBusiness CloseCard(CardDtoBusiness cardDtoBusiness);

        /// <summary>
        /// Changes card status to Cancelled by account number recieved in an object
        /// </summary>
        /// <param name="cardDtoBusiness"><see cref="cardDtoBusiness"/></param>
        /// <returns></returns>
        CardDtoBusiness CancelCard(CardDtoBusiness cardDtoBusiness);

        /// <summary>
        /// Generates card number depending on the card level as start then generte randomy - card number is 16 digits
        /// </summary>
        /// <param name="newCardDtoBusiness"><see cref="newCardDtoBusiness"/></param>
        /// <returns></returns>
        string GenerateCardNumber(NewCardDtoBusiness newCardDtoBusiness);

        /// <summary>
        /// Gets the extra info for each card such as benefits, Limits .. Etc
        /// </summary>
        /// <returns></returns>
        List<CardLevelDtoBusiness> GetNewCardExtraInfo();
    }
}
