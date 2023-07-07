using Microsoft.AspNetCore.Mvc;
using Analysis.Models;
using Requests;
using Responses;
using Interfaces;
using System.Threading.Tasks;

namespace Controllers.v1
{
    [ApiController]
    [Route("api/event")]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;

        public EventController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        [HttpGet("{id}")]
        public async Task<ResponseModel<GetEventResponse>> GetEventById(long id)
        {
            return await _eventRepository.GetEventById(id);
        }

        [HttpGet]
        public async Task<ResponseModel<PaginatedListModel<GetEventResponse>>> GetEvents(int page, int pageSize, [FromBody] GetEventsRequest request)
        {
            return await _eventRepository.GetEvents(page, pageSize, request);
        }

        [HttpPost]
        public async Task<ResponseModel<bool>> CreateEvent(string token, [FromBody] CreateEventRequest request)
        {
            return await _eventRepository.CreateEvent(token, request);
        }

        [HttpPut]
        public async Task<ResponseModel<bool>> UpdateEvent(string token, [FromBody] UpdateEventRequest request)
        {
            return await _eventRepository.UpdateEvent(token, request);
        }

        [HttpDelete("{id}")]
        public async Task<ResponseModel<bool>> DeleteEvent(long id, string token)
        {
            return await _eventRepository.DeleteEvent(id, token);
        }
    }
}
