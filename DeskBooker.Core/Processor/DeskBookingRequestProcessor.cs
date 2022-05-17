using DeskBooker.Core.DataInterface;
using DeskBooker.Core.Domain;
using System;

namespace DeskBooker.Core.Processor
{
    public class DeskBookingRequestProcessor
    {
        private readonly IDeskBookingRepository _deskBookingRepository;

        public DeskBookingRequestProcessor(IDeskBookingRepository deskBookingRepository)
        {
            _deskBookingRepository = deskBookingRepository;
        }

        public DeskBookingResult BookDesk(DeskBookingRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            _deskBookingRepository.SaveBooking(Create<DeskBooking>(request));

            return Create<DeskBookingResult>(request);

        }

        private static T Create<T>(DeskBookingRequest request) where T : DeskBookingBase, new()
        {
            return new T
            {
                Date = request.Date,
                Email = request.Email,
                LastName = request.LastName,
                FirstName = request.FirstName
            };
        }
    }
}