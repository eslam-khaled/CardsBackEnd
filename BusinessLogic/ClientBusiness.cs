using BusinessLogic.BusinessInterfaces;
using DTOs.BusinessDTOs;
using EPLHouse.Test.DataAccess.Models;
using EPLHouse.Test.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class ClientBusiness : IClientBusiness
    {
        private readonly IBaseRepository<Client> _client;
        private AppDBContext _context;

        /// <summary>
        /// Injecting DBcontext , IbaseRepository with model Client,
        /// IbaseRepository with model Account
        /// </summary>
        /// <param name="client"></param>
        /// <param name="context"></param>
        public ClientBusiness(IBaseRepository<Client> client, AppDBContext context)
        {
            _client = client;
            _context = context;
        }

        /// <inheritdoc/>
        public NewCardDtoBusiness CheckAccountNumber(NewCardDtoBusiness newCardDtoBusiness)
        {
            if (newCardDtoBusiness == null)
            {
                throw new Exception("Account data can't be null!");
            }
            Account account = new Account();
            if (newCardDtoBusiness.accountNumber.Length == 13 && newCardDtoBusiness.accountNumber != null)
            {
                account.AccountNumber = newCardDtoBusiness.accountNumber.ToString();
            }
            else
            {
                throw new Exception("Wrong account number!");
            }
            return newCardDtoBusiness;
        }

        /// <inheritdoc/>
        public NewCardDtoBusiness CheckFirstAndLastNames(NewCardDtoBusiness newCardDtoBusiness)
        {
            if (newCardDtoBusiness == null)
            {
                throw new Exception("Client data can't be null!");
            }

            Card card = new Card();
            if (string.IsNullOrEmpty(newCardDtoBusiness.firstNameOnCard) || string.IsNullOrEmpty(newCardDtoBusiness.secondNameOnCard) || string.IsNullOrEmpty(newCardDtoBusiness.familyNameOnCard))
            {
                throw new Exception("Please enter a valid name!");
            }
            else
            {
                card.firstNameOnCard = newCardDtoBusiness.firstNameOnCard;
                card.secondNameOnCard = newCardDtoBusiness.secondNameOnCard;
                card.familyNameOnCard = newCardDtoBusiness.familyNameOnCard;
            }
            return newCardDtoBusiness;
        }

        /// <inheritdoc/>
        public ClientDtoBusiness EditClientAddress(ClientDtoBusiness clientDtoBusiness)
        {
            if (clientDtoBusiness == null)
            {
                throw new Exception("Client info can't be null!");
            }
            if (string.IsNullOrEmpty(clientDtoBusiness.Address))
            {
                throw new Exception("Address can't be null!");
            }
            var ClientAccountOwner = _context.Clients.Where(c => c.account.FirstOrDefault().AccountNumber == clientDtoBusiness.accountNumber).Include(y => y.account).FirstOrDefault();
            ClientAccountOwner.Address = clientDtoBusiness.Address;
            _client.Update(ClientAccountOwner);
            return clientDtoBusiness;
        }

        /// <inheritdoc/>
        public ClientDtoBusiness GetClientDetailsByAccountNumber(string accountNumber)
        {
            if (accountNumber == null)
            {
                throw new Exception("Wrong account number!");
            }
            else if (accountNumber.Length != 6 && accountNumber.Length != 13)
            {
                throw new Exception("Wrong account number!");
            }

            ClientDtoBusiness clientDtoBusiness = new ClientDtoBusiness();

            Client client = null;


            if (accountNumber.Length == 6)
            {
                client = _context.Clients.Include(c => c.account).Where(x => x.account.FirstOrDefault().AccountNumber.Substring(4, 6) == accountNumber).FirstOrDefault();
            }
            else if (accountNumber.Length == 13)
            {
                client = _context.Clients.Include(c => c.account).Where(x => x.account.FirstOrDefault().AccountNumber == accountNumber).FirstOrDefault();
            }
            clientDtoBusiness.firstName = client.firstName;
            clientDtoBusiness.SecondName = client.SecondName;
            clientDtoBusiness.FamilyName = client.FamilyName;
            clientDtoBusiness.Address = client.Address;
            clientDtoBusiness.BirthDate = client.BirthDate;
            clientDtoBusiness.PersonIdentity = client.PersonIdentity;
            clientDtoBusiness.personIdentityType = client.personIdentityType;
            clientDtoBusiness.Age = client.Age;
            clientDtoBusiness.accountNumber = client.account.FirstOrDefault().AccountNumber;

            return clientDtoBusiness;
        }

        /// <inheritdoc/>
        public ClientDtoBusiness GetClientDetailsByCardNumber(string cardNumber)
        {
            if (cardNumber == null || cardNumber.Length != 16)
            {
                throw new Exception("Wrong card number!");
            }
            ClientDtoBusiness clientDtoBusiness = new ClientDtoBusiness();
            var SelectedCard = _context.Cards.Where(x => x.CardNumber == cardNumber).Include(c => c.Client).Include(A => A.Accounts).FirstOrDefault();

            clientDtoBusiness.firstName = SelectedCard.Client.firstName;
            clientDtoBusiness.SecondName = SelectedCard.Client.SecondName;
            clientDtoBusiness.FamilyName = SelectedCard.Client.FamilyName;
            clientDtoBusiness.accountNumber = SelectedCard.Client.account.FirstOrDefault().AccountNumber;
            clientDtoBusiness.PersonIdentity = SelectedCard.Client.PersonIdentity;
            clientDtoBusiness.personIdentityType = SelectedCard.Client.personIdentityType;
            clientDtoBusiness.BirthDate = SelectedCard.Client.BirthDate;
            clientDtoBusiness.Age = SelectedCard.Client.Age;
            clientDtoBusiness.Address = SelectedCard.Client.Address;

            return clientDtoBusiness;
        }

        /// <inheritdoc/>
        public ClientDtoBusiness GetClientDetailsIdentityNumber(string identityNumber)
        {
            if (identityNumber == null || identityNumber.Length != 12)
            {
                throw new Exception("Wrong identity number!");
            }

            ClientDtoBusiness clientDtoBusiness = new ClientDtoBusiness();
            var clientDetails = _context.Clients.Where(x => x.PersonIdentity == identityNumber).Include(c => c.account).FirstOrDefault();

            clientDtoBusiness.firstName = clientDetails.firstName;
            clientDtoBusiness.SecondName = clientDetails.SecondName;
            clientDtoBusiness.FamilyName = clientDetails.FamilyName;
            clientDtoBusiness.accountNumber = clientDetails.account.FirstOrDefault().AccountNumber;
            clientDtoBusiness.PersonIdentity = clientDetails.PersonIdentity;
            clientDtoBusiness.personIdentityType = clientDetails.personIdentityType;
            clientDtoBusiness.BirthDate = clientDetails.BirthDate;
            clientDtoBusiness.Age = clientDetails.Age;
            clientDtoBusiness.Address = clientDetails.Address;

            return clientDtoBusiness;
        }
    }
}
