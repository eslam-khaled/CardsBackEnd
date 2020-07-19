using BusinessLogic;
using BusinessLogic.BusinessInterfaces;
using DTOs.BusinessDTOs;
using Moq;
using Xunit;

namespace EPLHouse.Cards.Business.Tests
{
    public class ClientBusinessTests
    {
        [Fact]
        public void CheckAccountNumber_AccountLengthIsValid()
        {
            NewCardDtoBusiness NewCardDtoBusiness = new NewCardDtoBusiness();

            NewCardDtoBusiness.accountNumber = "1234567891234";

            var mockIClientBusiness = new Mock<IClientBusiness>();
            mockIClientBusiness.Setup(x => x.CheckAccountNumber(NewCardDtoBusiness)).Returns(NewCardDtoBusiness);

            IClientBusiness clientBusiness = mockIClientBusiness.Object;
            var ActualResult = clientBusiness.CheckAccountNumber(NewCardDtoBusiness);
            Assert.Equal(NewCardDtoBusiness, ActualResult);
        }

        [Fact]
        public void CheckFirstAndLastNames_NamesAreValid()
        {
            NewCardDtoBusiness newCardDtoBusiness = new NewCardDtoBusiness();

            newCardDtoBusiness.firstNameOnCard = "Eslam";
            newCardDtoBusiness.firstNameOnCard = "Khaled";

            var mockIClientBusiness = new Mock<IClientBusiness>();
            mockIClientBusiness.Setup(x => x.CheckFirstAndLastNames(newCardDtoBusiness)).Returns(newCardDtoBusiness);

            IClientBusiness clientBusiness = mockIClientBusiness.Object;
            var ActualResult = clientBusiness.CheckFirstAndLastNames(newCardDtoBusiness);

            Assert.Equal(newCardDtoBusiness, ActualResult);
        }
    }
}
