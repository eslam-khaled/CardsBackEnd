using DTOs.BusinessDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.BusinessInterfaces
{
    public interface IClientBusiness
    {
        /// <summary>
        /// Checks if the first name and last name are valid as they are not null
        /// </summary>
        /// <param name="businessClientDto"><see cref="client"></see></param>
        /// <param name="client"><see cref="businessClientDto"></see></param>
        NewCardDtoBusiness CheckFirstAndLastNames(NewCardDtoBusiness newCardDtoBusiness);

        /// <summary>
        /// checkes if the account number = 13 digits
        /// </summary>
        /// <param name="businessClientDto"><see cref="businessClientDto"></see></param>
        /// <returns></returns>
        NewCardDtoBusiness CheckAccountNumber(NewCardDtoBusiness newCardDtoBusiness);

        /// <summary>
        /// Get one client details by his account number
        /// </summary>
        /// <param name="accountNumber"><see cref="accountNumber"/></param>
        /// <returns></returns>
        ClientDtoBusiness GetClientDetailsByAccountNumber(string accountNumber);

        /// <summary>
        /// get one client details by card number
        /// </summary>
        /// <param name="cardNumber"><see cref="cardNumber"></see></param>
        /// <returns></returns>
        ClientDtoBusiness GetClientDetailsByCardNumber(string cardNumber);

        /// <summary>
        /// get one client details by identity number
        /// </summary>
        /// <param name="identityNumber"><see cref="identityNumber"></see></param>
        /// <returns></returns>
        ClientDtoBusiness GetClientDetailsIdentityNumber(string identityNumber);

        /// <summary>
        /// Edit client's address by account number
        /// </summary>
        /// <param name="clientDtoBusiness"><see cref="clientDtoBusiness"/></param>
        /// <returns></returns>
        ClientDtoBusiness EditClientAddress(ClientDtoBusiness clientDtoBusiness);
    }
}
