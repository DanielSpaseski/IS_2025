﻿using CoursesApplication.Repository.Interface;
using EventsApplication.Domain.DomainModels;
using EventsApplication.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace EventsApplication.Service.Implementation
{
    public class RegistrationService : IRegisrationService
    {
        private readonly IRepository<Registration> _registrationRepository;
        private readonly IAttendeeService _attendeeService;
        private readonly IEventService _eventService;
        private readonly IRepository<Schedule> _scheduleRepository;
        private readonly IRepository<EventInSchedule> _eventsInScheduleRepository;

        public RegistrationService(IRepository<Registration> registrationRepository, IAttendeeService attendeeService, IEventService eventService, IRepository<Schedule> scheduleRepository, IRepository<EventInSchedule> eventInScheduleRepository)
        {
            _registrationRepository = registrationRepository;
            _attendeeService = attendeeService;
            _eventService = eventService;
            _scheduleRepository = scheduleRepository;
            _eventsInScheduleRepository = eventInScheduleRepository;
        }

        public List<Registration> GetAll()
        {
            return _registrationRepository
                .GetAll(
                    selector: x => x,
                    include: x => x.Include(z => z.Event).Include(z => z.Owner).Include(z => z.Attendee)
                ).ToList();
        }
        public List<Registration> GetAllByCurrentUser(string userId)
        {
            return _registrationRepository
                .GetAll(
                    selector: x => x,
                    predicate: x => x.OwnerId.Equals(userId),
                    include: x => x.Include(z => z.Event).Include(z => z.Owner).Include(z => z.Attendee)
                ).ToList();
        }

        public Registration? GetById(Guid id)
        {
            return _registrationRepository
                .Get(
                    selector: x => x,
                    predicate: x => x.Id.Equals(id),
                    include: x => x.Include(z => z.Event).Include(z => z.Owner).Include(z => z.Attendee)
                );
        }

        public Registration DeleteById(Guid id)
        {
            // TODO: Implement method
            var registration = this.GetById(id);
            return _registrationRepository.Delete( registration );
        }

        public Registration RegisterAttendeeOnEvent(string userId, Guid attendeeId, Guid eventId, string paymentStatus)
        {
            var attendee = _attendeeService.GetById(attendeeId);
            var @event = _eventService.GetById(eventId);
            if (attendee == null || @event == null)
            {
                throw new Exception("Attendee or Event not found");
            }

            var registration = new Registration
            {
                Id = Guid.NewGuid(),
                OwnerId = userId,
                AttendeeId = attendeeId,
                EventId = eventId,
                PaymentStatus = paymentStatus
            };
            return _registrationRepository.Insert(registration);
        }

        public Schedule CreateSchedule(string userId)
        {
            // TODO: Implement method
            // Hint: Look at OrderProducts method in auditory exercises

            // Get all registrations by current user
            // Create new schedule and insert in database
            // Create new events in schedule and insert in database
            // Delete all registratons
            // Return schedule

            var registrations = this.GetAllByCurrentUser(userId);
            var newSchedule = new Schedule
            {
                Id = Guid.NewGuid(),
                OwnerId = userId
            };
            _scheduleRepository.Insert(newSchedule);

            var eventsInSchedule = registrations.Select(z => new EventInSchedule
            {
                ScheduleId = newSchedule.Id,
                Schedule = newSchedule,
                EventId = z.EventId,
                Event = z.Event
            }).ToList();

            foreach(var schedule in eventsInSchedule)
            {
                _eventsInScheduleRepository.Insert(schedule);
            }

            foreach (var registration in registrations)
            {
                _registrationRepository.Delete(registration);
            }
            return newSchedule;
        }
    }
}