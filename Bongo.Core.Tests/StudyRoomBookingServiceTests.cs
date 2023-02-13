using Bongo.Core.Services;
using Bongo.DataAccess.Repository.IRepository;
using Bongo.Models.Model;
using Bongo.Models.Model.VM;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bongo.Core
{
    [TestFixture]
    public class StudyRoomBookingServiceTests
    {
        private StudyRoomBooking _request;
        private List<StudyRoom> _availableRooms;
        private Mock<IStudyRoomBookingRepository> _studyRoomBookingRepoMock;
        private Mock<IStudyRoomRepository> _studyRoomRepoMock;

        private StudyRoomBookingService _bookingService;

        [SetUp]
        public void Setup()
        {
            _request = new StudyRoomBooking
            {
                FirstName = "Ben",
                LastName = "Spark",
                Email = "ben@gmail.com",
                Date = new DateTime(2023, 1, 1)
            };

            _availableRooms = new List<StudyRoom>
            {
                new StudyRoom
                {
                    Id = 10,
                    RoomName = "Michiegan",
                    RoomNumber = "A202"
                }
            };

            _studyRoomBookingRepoMock = new Mock<IStudyRoomBookingRepository>();
            _studyRoomRepoMock = new Mock<IStudyRoomRepository>();
            _studyRoomRepoMock.Setup(x => x.GetAll()).Returns(_availableRooms);

            _bookingService = new StudyRoomBookingService(
                _studyRoomBookingRepoMock.Object,
                _studyRoomRepoMock.Object);
        }

        [TestCase]
        public void GetAllBooking_InvokeMethod_CheckIfRepoIsCalled()
        {
            _bookingService.GetAllBooking();
            _studyRoomBookingRepoMock.Verify(x => x.GetAll(null), Times.Once);
        }

        [TestCase]
        public void BookingException_NullRequest_ThrowsException()
        {
            var exception = Assert.Throws<ArgumentNullException>(
                () => _bookingService.BookStudyRoom(null));

            Assert.AreEqual("Value cannot be null. (Parameter 'request')", exception.Message);
            Assert.AreEqual("request", exception.ParamName);
        }

        [Test]
        public void StudyRoomBooking_SaveBookingWithAvailableRoom_ReturnsResultWithAllValues()
        {
            // Arrange
            StudyRoomBooking studyRoomBooking = null;
            _studyRoomBookingRepoMock.Setup(x => x.Book(It.IsAny<StudyRoomBooking>()))
                .Callback<StudyRoomBooking>(booking =>
                {
                    studyRoomBooking = booking;
                });

            // Act
            _bookingService.BookStudyRoom(_request);

            // Assert
            _studyRoomBookingRepoMock.Verify(x => x.Book(It.IsAny<StudyRoomBooking>()), Times.Once);

            Assert.NotNull(studyRoomBooking);
            Assert.AreEqual(_request.FirstName, studyRoomBooking.FirstName);
            Assert.AreEqual(_request.LastName, studyRoomBooking.LastName);
            Assert.AreEqual(_request.Email, studyRoomBooking.Email);
            Assert.AreEqual(_request.Date, studyRoomBooking.Date);
            Assert.AreEqual(_availableRooms.First().Id, studyRoomBooking.StudyRoomId);
        }

        [Test]
        public void StudyRoomBookingResultCheck_InputRequest_ValuesMatchInResult()
        {
            StudyRoomBookingResult result = _bookingService.BookStudyRoom(_request);

            Assert.NotNull(result);
            Assert.AreEqual(_request.FirstName, result.FirstName);
            Assert.AreEqual(_request.LastName, result.LastName);
            Assert.AreEqual(_request.Email, result.Email);
            Assert.AreEqual(_request.Date, result.Date);
        }

        [TestCase(true, ExpectedResult = StudyRoomBookingCode.Success)]
        [TestCase(false, ExpectedResult = StudyRoomBookingCode.NoRoomAvailable)]
        public StudyRoomBookingCode ResultCodeSuccess_RoomAvailbility_ReturnsSuccessResultCode(bool roomAvailability)
        {
            if (!roomAvailability)
            {
                _availableRooms.Clear();
            }
            return _bookingService.BookStudyRoom(_request).Code;
            //Assert.AreEqual(StudyRoomBookingCode.Success, result.Code);
        }

        [TestCase(0,false)]
        [TestCase(55, true)]
        public void StudyRoomBooking_SaveBookingWithAvailability_ReturnsBookingId(int expectedBookingId, bool roomAvailability)
        {
            if (!roomAvailability)
            {
                _availableRooms.Clear();
            }

            _studyRoomBookingRepoMock.Setup(x => x.Book(It.IsAny<StudyRoomBooking>()))
                .Callback<StudyRoomBooking>(booking =>
                {
                    booking.BookingId = 55;
                });

            var result = _bookingService.BookStudyRoom(_request);
            Assert.AreEqual(expectedBookingId, result.BookingId);

            if (!roomAvailability)
            {
                _studyRoomBookingRepoMock.Verify(x => x.Book(It.IsAny<StudyRoomBooking>()), Times.Never);
            }
        }

        [Test]
        public void BookNotInvoked_SaveBookingWithouAvailableRoom_BookMethodNotInvoked()
        {
            _availableRooms.Clear();

            var result = _bookingService.BookStudyRoom(_request);
            _studyRoomBookingRepoMock.Verify(x => x.Book(It.IsAny<StudyRoomBooking>()), Times.Never);
        }
    }
}
