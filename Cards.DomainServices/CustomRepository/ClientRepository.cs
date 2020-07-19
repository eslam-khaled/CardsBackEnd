using Cards.DomainServices.Services;
using DTOs.BusinessDTOs;
using EPLHouse.Test.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPLHouse.Cards.DataAccess.CustomRepository
{
    public class ClientRepository: IClientRepository
    {
        private AppDBContext _context;
        public ClientRepository(AppDBContext context)
        {
            _context = context;
        }

        Client IClientRepository.PrepareClientByAccountNumber(NewCardDtoBusiness newCardDtoBusiness)
        {
            Client client = _context.Clients.Where(c => c.account.FirstOrDefault().AccountNumber == newCardDtoBusiness.accountNumber).FirstOrDefault();
            return client;
        }
    }
}
