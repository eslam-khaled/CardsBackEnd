using Cards.DomainServices.Services;
using DTOs.BusinessDTOs;
using DTOs.Enums;
using EPLHouse.Cards.DataAccess.Models;
using EPLHouse.Test.DataAccess.Models;
using EPLHouse.Test.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    public class CardBusiness : ICardBusiness
    {
        private readonly IConfiguration _configuration;
        private readonly IBaseRepository<Card> _cardBasRepository;
        private readonly IBaseRepository<Account> _accountBaseRepository;
        private readonly IBaseRepository<CardsLevel> _cardsLevelbaseRepository;
        private readonly IClientRepository _clientRepository;

        /// <summary>
        /// Injecting DBcontext , IbaseRepository with model Client, IbaseRepository with model Card,
        /// IbaseRepository with model Account
        /// </summary>
        /// <param name="client"><see cref="client"/></param>
        /// <param name="cardBasRepository"><see cref="card"/></param>
        /// <param name="accountBaseRepository"><see cref="account"/></param>
        /// <param name="context"><see cref="context"/></param>
        public CardBusiness(
            IConfiguration configuration,
            IBaseRepository<Card> cardBasRepository,
            IBaseRepository<Account> accountBaseRepository,
            IClientRepository clientRepository,
            IBaseRepository<CardsLevel> cardsLevelbaseRepository)
        {
            _configuration = configuration;
            _cardBasRepository = cardBasRepository;
            _accountBaseRepository = accountBaseRepository;
            _clientRepository = clientRepository;
            _cardsLevelbaseRepository = cardsLevelbaseRepository;
        }



        private int generatRandom(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }



        public string GenerateCardNumber(NewCardDtoBusiness newCardDtoBusiness)
        {
            int StartSevenNumbers = Convert.ToInt32(_configuration["NumberGenerator:StartSevenNumbers"]);
            int EndSevenNumbers = Convert.ToInt32(_configuration["NumberGenerator:EndSevenNumbers"]);
            int StartThreeNumbers = Convert.ToInt32(_configuration["NumberGenerator:StartThreeNumbers"]);
            int EndThreeNumbers = Convert.ToInt32(_configuration["NumberGenerator:EndThreeNumbers"]);
            string generatedCardNumber = "";

            var StartOfCardNumber = _cardsLevelbaseRepository.GetWhere(x => x.Id == (int)newCardDtoBusiness.cardLevel).FirstOrDefault().StartOfCardNumber;

            CardsLevel cardsLevel = new CardsLevel();
            if (newCardDtoBusiness.cardLevel == CardLevelEnum.Electron)
            {
                generatedCardNumber = StartOfCardNumber + generatRandom(StartSevenNumbers, EndSevenNumbers).ToString() + generatRandom(StartThreeNumbers, EndThreeNumbers).ToString();
            }
            else if (newCardDtoBusiness.cardLevel == CardLevelEnum.Gold)
            {
                generatedCardNumber = StartOfCardNumber + generatRandom(StartSevenNumbers, EndSevenNumbers).ToString() + generatRandom(StartThreeNumbers, EndThreeNumbers).ToString();
            }
            else if (newCardDtoBusiness.cardLevel == CardLevelEnum.Platinum)
            {
                generatedCardNumber = StartOfCardNumber + generatRandom(StartSevenNumbers, EndSevenNumbers).ToString() + generatRandom(StartThreeNumbers, EndThreeNumbers).ToString();
            }
            else if (newCardDtoBusiness.cardLevel == CardLevelEnum.Classic)
            {
                generatedCardNumber = StartOfCardNumber + generatRandom(StartSevenNumbers, EndSevenNumbers).ToString();
            }
            else if (newCardDtoBusiness.cardLevel == CardLevelEnum.gold)
            {
                generatedCardNumber = StartOfCardNumber + generatRandom(1111111, 9999999).ToString();
            }
            else if (newCardDtoBusiness.cardLevel == CardLevelEnum.platinum)
            {
                generatedCardNumber = StartOfCardNumber + generatRandom(1111111, 9999999).ToString();
            }

            Helper.Helper.Encrypt(generatedCardNumber, "E01P10L99");
            return generatedCardNumber;
        }

        /// <inheritdoc/>
        public NewCardDtoBusiness CreateNewCard(NewCardDtoBusiness newCardDtoBusiness)
        {
            if (newCardDtoBusiness == null)
            {
                throw new Exception("Received data can't be null!");
            }

            CardDtoBusiness cardDtoBusiness = new CardDtoBusiness();
            Account account = _accountBaseRepository.GetWhere(x => x.AccountNumber == newCardDtoBusiness.accountNumber).FirstOrDefault();
            
            Card card = new Card();

            CheckFirstAndLastNames(newCardDtoBusiness);
            CheckAccountNumber(newCardDtoBusiness);

            CheckIfSubscribedOn084Program(account);
            card.firstNameOnCard = newCardDtoBusiness.firstNameOnCard;
            card.secondNameOnCard = newCardDtoBusiness.secondNameOnCard;
            card.familyNameOnCard = newCardDtoBusiness.familyNameOnCard;

            card.MajorOrSub = (int)newCardDtoBusiness.majorOrSub;
            card.Comment = newCardDtoBusiness.comment;
            card.DeliveryBranch = newCardDtoBusiness.deliveryBranch;
            card.StartDate = DateTime.Now;
            DateTime ExDate = DateTime.Today.AddYears(3);
            card.CardExpiryDate = ExDate;
            card.CVV = generatRandom(111, 999);
            card.ICVV = generatRandom(111, 999);
            card.CVV_2 = generatRandom(111, 999);
            card.PIN = generatRandom(1111, 9999);

            card.CardNumber = GenerateCardNumber(newCardDtoBusiness).ToString();
            card.CardStatus = (int)CardStatusEnum.Valid;

            card.Client_Id = account.Client_Id;

            card.CardsLevel_Id = (int)newCardDtoBusiness.cardLevel;
            ChargeOnIssueNewCard(newCardDtoBusiness);
            card.Accounts = account;
            card.ReadyToPrint = true;
            SendRequestToSMT(card);
            _cardBasRepository.AddNew(card);
            return newCardDtoBusiness;
        }

        /// <inheritdoc/>
        public bool SendRequestToSMT(Card card)
        {
            return true;
        }

        /// <inheritdoc/>
        public Account ChargeOnIssueNewCard(NewCardDtoBusiness newCardDtoBusiness)
        {
            Account account = new Account();
            if (account == null)
            {
                throw new Exception("Account data can't be null!");
            }
            //var x = newCardDtoBusiness.cardLevelId.ToString();
            var selectedAccount = _accountBaseRepository.GetWhere(x => x.AccountNumber == newCardDtoBusiness.accountNumber).FirstOrDefault();
            var SelectedCardLevel = _cardsLevelbaseRepository.GetWhere(c => c.cardLevel == newCardDtoBusiness.cardLevel.ToString()).FirstOrDefault();

            selectedAccount.Balance = selectedAccount.Balance - SelectedCardLevel.IssueFee;
            _accountBaseRepository.Update(selectedAccount);
            return account;
        }

        /// <inheritdoc/>
        public bool CheckIfSubscribedOn084Program(Account account)
        {
            if (account == null)
            {
                throw new Exception("Account data can't be null!");
            }

            if (account.IsIn084Account == false)
            {
                throw new Exception("You are not subscribed in 084!");
            }
            return true;

        }

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
        public ClientDtoBusiness ReplaceCardByAccountNumber(ClientDtoBusiness clientDtoBusiness)
        {
            if (clientDtoBusiness == null)
            {
                throw new Exception("Client data can't be null!");
            }

            Card cards = new Card();
            if (clientDtoBusiness.accountNumber.Length == 13)
            {
                var clientAccount = _cardBasRepository.GetWhere(x => x.Accounts.AccountNumber == clientDtoBusiness.accountNumber).Include(c => c.Accounts).ToList().FirstOrDefault();
                clientAccount.CVV = generatRandom(111, 999);
                clientAccount.PIN = generatRandom(1111, 9999);
                cards = clientAccount;
                _cardBasRepository.Update(cards);
            }
            else

            {
                throw new Exception("Account number is invalid!");
            }
            return clientDtoBusiness;
        }

        /// <inheritdoc/>
        public CardDtoBusiness ReplaceCardByCardNumber(CardDtoBusiness cardDtoBusiness)
        {
            if (cardDtoBusiness.cardNumber != null && cardDtoBusiness.cardNumber.Length == 16)
            {
                Card cards = new Card();

                var clientAccount = _cardBasRepository.GetWhere(x => x.CardNumber == cardDtoBusiness.cardNumber).FirstOrDefault();
                clientAccount.CVV = generatRandom(111, 999);
                clientAccount.PIN = generatRandom(1111, 9999);
                cardDtoBusiness.CardExpiryDate = clientAccount.CardExpiryDate;
                cardDtoBusiness.cardNumber = clientAccount.CardNumber;
                cardDtoBusiness.CCV = clientAccount.CVV;
                cardDtoBusiness.PIN = clientAccount.PIN;
                cardDtoBusiness.StartDate = clientAccount.StartDate;
                cardDtoBusiness.CardExpiryDate = clientAccount.CardExpiryDate;
                _cardBasRepository.Update(clientAccount);
                return cardDtoBusiness;
            }
            else
            {
                throw new Exception("Card number is invalid!");
            }
        }

        /// <inheritdoc/>
        public CardDtoBusiness ReplaceCardByIdentity(string IdentityNumber)
        {
            if (IdentityNumber.Length == 12 && IdentityNumber != null)
            {
                Card cards = new Card();
                ClientDtoBusiness clientDtoBusiness = new ClientDtoBusiness();
                CardDtoBusiness cardDtoBusiness = new CardDtoBusiness();

                var clientAccount = _cardBasRepository.GetWhere(x => x.Client.PersonIdentity == IdentityNumber).FirstOrDefault();
                clientAccount.CVV = generatRandom(111, 999);
                clientAccount.PIN = generatRandom(1111, 9999);
                cardDtoBusiness.CardExpiryDate = clientAccount.CardExpiryDate;
                cardDtoBusiness.cardNumber = clientAccount.CardNumber;
                cardDtoBusiness.CCV = clientAccount.CVV;
                cardDtoBusiness.PIN = clientAccount.PIN;
                cardDtoBusiness.StartDate = clientAccount.StartDate;
                cardDtoBusiness.CardExpiryDate = clientAccount.CardExpiryDate;

                _cardBasRepository.Update(clientAccount);
                return cardDtoBusiness;
            }
            else
            {
                throw new Exception("Identity number is invalid!");
            }
        }

        /// <inheritdoc/>
        public ClientDtoBusiness ReNewByAccountNumber(string accountNumber)
        {
            Card cards = new Card();
            ClientDtoBusiness clientDtoBusiness = new ClientDtoBusiness();

            if (accountNumber.Length == 13 && accountNumber != null)
            {
                var clientAccount = _cardBasRepository.GetWhere(x => x.Accounts.AccountNumber == accountNumber).Include(c => c.Accounts).FirstOrDefault();
                clientAccount.CVV = generatRandom(111, 999);
                cards = clientAccount;
                clientDtoBusiness.accountNumber = accountNumber;
                _cardBasRepository.Update(cards);
                return clientDtoBusiness;
            }
            else
            {
                throw new Exception("Account number is invalid!");
            }
        }

        /// <inheritdoc/>
        public CardDtoBusiness ReNewByCardNumber(CardDtoBusiness cardDtoBusiness)
        {
            if (cardDtoBusiness.cardNumber.Length == 16 && cardDtoBusiness.cardNumber != null)
            {
                var ClientCard = _cardBasRepository.GetWhere(x => x.CardNumber == cardDtoBusiness.cardNumber).FirstOrDefault();
                ClientCard.CVV = generatRandom(111, 999);
                _cardBasRepository.Update(ClientCard);
                return cardDtoBusiness;
            }
            else
            {
                throw new Exception("Card number is invalid!");
            }
        }

        /// <inheritdoc/>
        public CardDtoBusiness ReNewByIdentity(string IdentityNumber)
        {
            CardDtoBusiness cardDtoBusiness = new CardDtoBusiness();
            if (IdentityNumber.Length == 12 && IdentityNumber != null)
            {
                var ClientCard = _cardBasRepository.GetWhere(x => x.Client.PersonIdentity == IdentityNumber).FirstOrDefault();
                ClientCard.CVV = generatRandom(111, 999);
                _cardBasRepository.Update(ClientCard);
                return cardDtoBusiness;
            }
            else
            {
                throw new Exception("Card number is invalid!");
            }
        }

        /// <inheritdoc/>
        public ClientDtoBusiness ReGenerateByAccountNumber(string accountNumber)
        {
            Card cards = new Card();
            if (accountNumber.Length == 13 && accountNumber != null)
            {
                ClientDtoBusiness clientDtoBusiness = new ClientDtoBusiness();
                var clientAccount = _cardBasRepository.GetWhere(x => x.Accounts.AccountNumber == accountNumber).Include(c => c.Accounts).FirstOrDefault();
                clientAccount.PIN = generatRandom(1111, 9999);
                _cardBasRepository.Update(clientAccount);
                clientDtoBusiness.accountNumber = accountNumber;
                return clientDtoBusiness;
            }
            else
            {
                throw new Exception("Account number is invalid!");
            }
        }

        /// <inheritdoc/>
        public CardDtoBusiness ReGenerateByCardNumber(CardDtoBusiness cardDtoBusiness)
        {

            Card cards = new Card();
            if (cardDtoBusiness.cardNumber.Length == 16 && cardDtoBusiness.cardNumber != null)
            {
                var clientCard = _cardBasRepository.GetWhere(x => x.CardNumber == cardDtoBusiness.cardNumber).FirstOrDefault();
                clientCard.PIN = generatRandom(1111, 9999);
                _cardBasRepository.Update(clientCard);
                return cardDtoBusiness;
            }
            else
            {
                throw new Exception("Wrong card number!");
            }

        }

        /// <inheritdoc/>
        public CardDtoBusiness ReGenerateByIdentity(string IdentityNumber)
        {
            CardDtoBusiness cardDtoBusiness = new CardDtoBusiness();
            Card cards = new Card();
            if (IdentityNumber.Length == 12 && IdentityNumber != null)
            {
                var clientCard = _cardBasRepository.GetWhere(x => x.Client.PersonIdentity == IdentityNumber).FirstOrDefault();
                clientCard.PIN = generatRandom(1111, 9999);
                cardDtoBusiness.PIN = clientCard.PIN;
                _cardBasRepository.Update(clientCard);
                return cardDtoBusiness;
            }
            else
            {
                throw new Exception("Wrong card number!");
            }
        }

        /// <inheritdoc/>
        public List<CardDtoBusiness> GetCardsByAccountNumber(string accountNumber)
        {
            if (accountNumber == null)
            {
                throw new Exception("Wrong account number!");
            }
            else if (accountNumber.Length != 13 && accountNumber.Length != 6)
            {
                throw new Exception("Wrong account number!");
            }

            List<CardDtoBusiness> cardDtoBusinessList = new List<CardDtoBusiness>();
            List<Card> cards = null;
            if (accountNumber.Length == 6)
            {
                cards = _cardBasRepository.GetWhere(x => x.Accounts.AccountNumber.Substring(4, 6) == accountNumber).Include(c => c.Accounts).ToList();
            }
            else if (accountNumber.Length == 13)
            {
                cards = _cardBasRepository.GetWhere(x => x.Accounts.AccountNumber == accountNumber).OrderByDescending(b => b.Id).Include(c => c.Accounts).ToList();
            }

            foreach (var card in cards)
            {
                cardDtoBusinessList.Add(new CardDtoBusiness()
                {
                    cardNumber = card.CardNumber,
                    majorOrSub = (CardMajorOrSubEnum)Enum.Parse(typeof(CardMajorOrSubEnum), card.MajorOrSub.ToString()),
                    firstNameOnCard = card.firstNameOnCard,
                    secondNameOnCard = card.secondNameOnCard,
                    familyNameOnCard = card.familyNameOnCard,
                    StartDate = card.StartDate,
                    CardExpiryDate = card.CardExpiryDate,
                    CardStatus = (CardStatusEnum)Enum.Parse(typeof(CardStatusEnum), card.CardStatus.ToString()),

                    Id = card.Id,
                });
            }

            return cardDtoBusinessList;
        }

        /// <inheritdoc/>
        public List<CardDtoBusiness> GetCardsByCardNumber(string cardNumber)
        {
            if (cardNumber == null || cardNumber.Length != 16)
            {
                throw new Exception("Wrong card number!");
            }
            List<CardDtoBusiness> cardDtoBusinesses = new List<CardDtoBusiness>();
            Card ResultCardNumber = _cardBasRepository.GetWhere(x => x.CardNumber == cardNumber).Include(y => y.Accounts).FirstOrDefault();
            var resultByAccNumber = ResultCardNumber.Accounts.AccountNumber;
            List<Card> cardsList = _cardBasRepository.GetWhere(c => c.Accounts.AccountNumber == resultByAccNumber).OrderByDescending(b => b.Id).ToList();
            foreach (var card in cardsList)
            {
                cardDtoBusinesses.Add(new CardDtoBusiness()
                {
                    Id = card.Id,
                    CardExpiryDate = card.CardExpiryDate,
                    cardNumber = card.CardNumber,
                    firstNameOnCard = card.firstNameOnCard,
                    secondNameOnCard = card.secondNameOnCard,
                    familyNameOnCard = card.familyNameOnCard,
                    majorOrSub = (CardMajorOrSubEnum)Enum.Parse(typeof(CardMajorOrSubEnum), card.MajorOrSub.ToString()),
                    StartDate = card.StartDate,
                    CardStatus = (CardStatusEnum)Enum.Parse(typeof(CardStatusEnum), card.CardStatus.ToString()),
                    accountNumber = card.Accounts.AccountNumber,
                });
            }

            return cardDtoBusinesses;
        }

        /// <inheritdoc/>
        public List<CardDtoBusiness> GetCardsByIdentityNumber(string identityNumber)
        {
            if (identityNumber == null || identityNumber.Length != 12)
            {
                throw new Exception("Wrong Identity number!");
            }

            List<CardDtoBusiness> cardDtoBusinesses = new List<CardDtoBusiness>();
            List<Card> cardsList = _cardBasRepository.GetWhere(x => x.Client.PersonIdentity == identityNumber).OrderByDescending(b => b.Id).Include(c => c.Accounts).ToList();
            foreach (var card in cardsList)
            {
                cardDtoBusinesses.Add(new CardDtoBusiness()
                {
                    firstNameOnCard = card.firstNameOnCard,
                    secondNameOnCard = card.secondNameOnCard,
                    familyNameOnCard = card.familyNameOnCard,
                    majorOrSub = (CardMajorOrSubEnum)Enum.Parse(typeof(CardMajorOrSubEnum), card.MajorOrSub.ToString()),
                    CardExpiryDate = card.CardExpiryDate,
                    cardNumber = card.CardNumber,
                    PIN = card.PIN,
                    StartDate = card.StartDate,
                    CardStatus = (CardStatusEnum)Enum.Parse(typeof(CardStatusEnum), card.CardStatus.ToString()),
                    accountNumber = card.Accounts.AccountNumber,
                });
            }

            return cardDtoBusinesses;
        }

        /// <inheritdoc/>
        public CardDtoBusiness GetCardDetailsById(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Wrong Id!");
            }
            CardDtoBusiness cardDtoBusiness = new CardDtoBusiness();
            var CardDetails = _cardBasRepository.GetWhere(x => x.Id == id).Include(c => c.Accounts).Include(y => y.Client).Include(L => L.cardsLevel).FirstOrDefault();
            cardDtoBusiness.accountNumber = CardDetails.Accounts.AccountNumber;
            var cardtype = _cardsLevelbaseRepository.GetWhere(T => T.cardLevel == CardDetails.cardsLevel.cardLevel).Include(p => p.cardsType).FirstOrDefault();
            cardDtoBusiness.cardType = cardtype.cardsType.cardType;
            cardDtoBusiness.cardNumber = CardDetails.CardNumber;
            cardDtoBusiness.StartDate = CardDetails.StartDate;
            cardDtoBusiness.CardExpiryDate = CardDetails.CardExpiryDate;
            cardDtoBusiness.majorOrSub = (CardMajorOrSubEnum)Enum.Parse(typeof(CardMajorOrSubEnum), CardDetails.MajorOrSub.ToString());
            var cardType = CardDetails.cardsLevel.cardsType.cardType;
            cardDtoBusiness.cardType = cardType;
            cardDtoBusiness.DailyPosLimit = CardDetails.cardsLevel.DailyPosLimit;
            cardDtoBusiness.DailyCashLimit = CardDetails.cardsLevel.DailyCashLimit;
            cardDtoBusiness.cardLevel = (CardLevelEnum)Enum.Parse(typeof(CardLevelEnum), CardDetails.cardsLevel.cardLevel.ToString());
            cardDtoBusiness.readyToPrint = CardDetails.ReadyToPrint;
            cardDtoBusiness.CardStatus = (CardStatusEnum)Enum.Parse(typeof(CardStatusEnum), CardDetails.CardStatus.ToString());
            cardDtoBusiness.WithdrawalLimit = CardDetails.WithdrawalLimit;
            cardDtoBusiness.Currency = CardDetails.Currency;
            cardDtoBusiness.buyingLimit = CardDetails.BuyingLimit;
            cardDtoBusiness.firstNameOnCard = CardDetails.firstNameOnCard;
            cardDtoBusiness.secondNameOnCard = CardDetails.secondNameOnCard;
            cardDtoBusiness.familyNameOnCard = CardDetails.familyNameOnCard;

            return cardDtoBusiness;
        }

        /// <inheritdoc/>
        public CardDtoBusiness CloseCard(CardDtoBusiness cardDtoBusiness)
        {
            if (cardDtoBusiness == null)
            {
                throw new Exception("Card number can't be null!");
            }
            var SelectedCard = _cardBasRepository.GetWhere(x => x.CardNumber == cardDtoBusiness.cardNumber).FirstOrDefault();
            SelectedCard.CardStatus = 2;
            _cardBasRepository.Update(SelectedCard);
            return cardDtoBusiness;
        }

        /// <inheritdoc/>
        public CardDtoBusiness CancelCard(CardDtoBusiness cardDtoBusiness)
        {
            if (cardDtoBusiness == null)
            {
                throw new Exception("Card number can't be null!");
            }
            var SelectedCard = _cardBasRepository.GetWhere(x => x.CardNumber == cardDtoBusiness.cardNumber).FirstOrDefault();
            SelectedCard.CardStatus = 3;
            _cardBasRepository.Update(SelectedCard);
            return cardDtoBusiness;
        }

        public List<CardLevelDtoBusiness> GetNewCardExtraInfo()
        {
            List<CardLevelDtoBusiness> cardLevelDtoBusinesses = new List<CardLevelDtoBusiness>();
            List<CardsLevel> CardsLevel = _cardsLevelbaseRepository.GetAll().ToList();
            foreach (var level in CardsLevel)
            {
                cardLevelDtoBusinesses.Add(new CardLevelDtoBusiness()
                {
                    CardBenefit = level.CardBenefit,
                    cardLevel = level.cardLevel,
                    DailyCashLimit = level.DailyCashLimit,
                    DailyPosLimit = level.DailyPosLimit,
                    Id = level.Id,
                    IssueFee = level.IssueFee,
                    StartOfCardNumber = level.StartOfCardNumber,
                });
            }
            return cardLevelDtoBusinesses;
        }
    }
}
