using DTOs.BusinessDTOs;
using EPLHouse.Test.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cards.DomainServices.Services
{
    public interface IClientRepository
    {
        Client PrepareClientByAccountNumber(NewCardDtoBusiness newCardDtoBusiness);
    }
}
