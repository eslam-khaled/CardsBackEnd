using BusinessLogic;
using DTOs.BusinessDTOs;
using Moq;
using Xunit;

namespace EPLHouse.Cards.Business.Tests
{
    public class CardBusinessTests
    {

        [Fact]
        public void ReplaceCardByAccountNumber_AccountNumberIsValid()
        {
            ClientDtoBusiness clientDtoBusiness = new ClientDtoBusiness();
            clientDtoBusiness.accountNumber = "1234567891234";

            var mockIClientBusiness = new Mock<ICardBusiness>();
            mockIClientBusiness.Setup(x => x.ReplaceCardByAccountNumber(clientDtoBusiness)).Returns(clientDtoBusiness);

            ICardBusiness clientBusiness = mockIClientBusiness.Object;
            var ActualResult = clientBusiness.ReplaceCardByAccountNumber(clientDtoBusiness);

            Assert.Equal(clientDtoBusiness, ActualResult);
        }
        [Fact]
        public void ReplaceCardByCardNumber_CardNumberIsValid()
        {
            CardDtoBusiness cardDtoBusiness = new CardDtoBusiness();
            cardDtoBusiness.cardNumber = "1234567891234567";

            var mockIClientBusiness = new Mock<ICardBusiness>();
            mockIClientBusiness.Setup(x => x.ReplaceCardByCardNumber(cardDtoBusiness)).Returns(cardDtoBusiness);

            ICardBusiness clientBusiness = mockIClientBusiness.Object;

            var ActualResult = clientBusiness.ReplaceCardByCardNumber(cardDtoBusiness);
            Assert.Equal(cardDtoBusiness, ActualResult);
        }

        [Fact]
        public void ReNewByAccountNumber_AccountNumberIsValid()
        {
            ClientDtoBusiness clientDtoBusiness = new ClientDtoBusiness();
            clientDtoBusiness.accountNumber = "1234567891234";

            var mockIClientBusiness = new Mock<ICardBusiness>();
            mockIClientBusiness.Setup(x => x.ReNewByAccountNumber(clientDtoBusiness.accountNumber)).Returns(clientDtoBusiness);

            ICardBusiness clientBusiness = mockIClientBusiness.Object;
            var ActualResult = clientBusiness.ReNewByAccountNumber(clientDtoBusiness.accountNumber);

            Assert.Equal(clientDtoBusiness, ActualResult);
        }

        [Fact]
        public void ReNewByCardNumber_CardNumberIsValid()
        {
            CardDtoBusiness cardDtoBusiness = new CardDtoBusiness();
            cardDtoBusiness.cardNumber = "1234567891234567";

            var mockIClientBusiness = new Mock<ICardBusiness>();
            mockIClientBusiness.Setup(x => x.ReNewByCardNumber(cardDtoBusiness)).Returns(cardDtoBusiness);

            ICardBusiness clientBusiness = mockIClientBusiness.Object;

            var ActualResult = clientBusiness.ReNewByCardNumber(cardDtoBusiness);
            Assert.Equal(cardDtoBusiness, ActualResult);
        }

        [Fact]
        public void ReGenerateByAccountNumber_AccountNumberIsValid()
        {
            ClientDtoBusiness clientDtoBusiness = new ClientDtoBusiness();
            clientDtoBusiness.accountNumber = "1234567891234";

            var mockIClientBusiness = new Mock<ICardBusiness>();
            mockIClientBusiness.Setup(x => x.ReGenerateByAccountNumber(clientDtoBusiness.accountNumber)).Returns(clientDtoBusiness);

            ICardBusiness clientBusiness = mockIClientBusiness.Object;
            var ActualResult = clientBusiness.ReGenerateByAccountNumber(clientDtoBusiness.accountNumber);

            Assert.Equal(clientDtoBusiness, ActualResult);
        }
        [Fact]
        public void ReGenerateByAccountNumber_CardNumberIsValid()
        {
            CardDtoBusiness cardDtoBusiness = new CardDtoBusiness();
            cardDtoBusiness.cardNumber = "1234567891234567";

            var mockIClientBusiness = new Mock<ICardBusiness>();
            mockIClientBusiness.Setup(x => x.ReGenerateByCardNumber(cardDtoBusiness)).Returns(cardDtoBusiness);

            ICardBusiness clientBusiness = mockIClientBusiness.Object;
            var ActualResult = clientBusiness.ReGenerateByCardNumber(cardDtoBusiness);

            Assert.Equal(cardDtoBusiness, ActualResult);
        }
    }
}
