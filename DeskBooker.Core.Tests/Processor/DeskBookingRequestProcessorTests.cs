using DeskBooker.Core.DataInterface;
using DeskBooker.Core.Domain;
using DeskBooker.Core.Processor;
using Moq;
using System;
using Xunit;

namespace DeskBooker.Core.Tests.Processor
{
    public class DeskBookingRequestProcessorTests
    {
        private readonly DeskBookingRequest _request;
        public readonly DeskBookingRequestProcessor _processor;
        private readonly Mock<IDeskBookingRepository> _repositoryMock;

        public DeskBookingRequestProcessorTests()
        {
            _request = new DeskBookingRequest()
            {
                FirstName = "Doğancan",
                LastName = "Kasap",
                Email = "dgncn.ksp@gmail.com",
                Date = new DateTime(2022, 05, 17)
            };

            _repositoryMock = new Mock<IDeskBookingRepository>();
            _processor = new DeskBookingRequestProcessor(_repositoryMock.Object);

        }


        [Fact]
        public void ShouldReturnDeskBookingResultWithRequestValues()
        {

            //Arrange
            DeskBookingResult result = _processor.BookDesk(_request);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(_request.FirstName, result.FirstName);
            Assert.Equal(_request.LastName, result.LastName);
            Assert.Equal(_request.Email, result.Email);
            Assert.Equal(_request.Date, result.Date);

        }

        [Fact]
        public void ShouldThrowWhenRequestNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => _processor.BookDesk(null));
            Assert.Equal("request", exception.ParamName);
        }

        [Fact]
        public void ShouldSaveDeskBooking()
        {
            DeskBooking savedDeskBooking = null;
            _repositoryMock.Setup(x => x.SaveBooking(It.IsAny<DeskBooking>()))
                .Callback<DeskBooking>(deskBooking =>
                {
                    savedDeskBooking = deskBooking;
                });
            _processor.BookDesk(_request);

            _repositoryMock.Verify(x => x.SaveBooking(It.IsAny<DeskBooking>()), Times.Once);
            Assert.NotNull(savedDeskBooking);
            Assert.Equal(_request.FirstName, savedDeskBooking.FirstName);
            Assert.Equal(_request.LastName, savedDeskBooking.LastName);
            Assert.Equal(_request.Email, savedDeskBooking.Email);
            Assert.Equal(_request.Date, savedDeskBooking.Date);




        }
    }
}
