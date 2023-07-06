using Analysis.Models;
using Requests;
using Responses;

namespace Interfaces;
public interface IEventRepository
{
    public Task<ResponseModel<GetEventResponse>> GetEventById(long Id);
    public Task<ResponseModel<PaginatedListModel<GetEventResponse>>> GetEvents(int page, int pageSize);
    public Task<ResponseModel<bool>> CreateEvent(string token, CreateEventRequest request);
    public Task<ResponseModel<bool>> UpdateEvent(string token, UpdateEventRequest request);
    public Task<ResponseModel<bool>> DeleteEvent(long Id, string token);
}