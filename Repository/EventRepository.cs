using Analysis.Enums;
using Analysis.Models;
using AnalysisService.Data;
using AutoMapper;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.DBTables;
using Requests;
using Responses;

namespace Repository;
public class EventRepository : IEventRepository
{
    private IConfiguration _configuration;
    private readonly IMapper _mapper;
    public EventRepository(IConfiguration configuration, IMapper mapper)
    {
        _configuration = configuration;
        _mapper = mapper;
    }

    public async Task<ResponseModel<bool>> CreateEvent(string token, CreateEventRequest request)
    {
        try
        {
            await using var context = new ApplicationContext(_configuration);
            var Event = _mapper.Map<EventModel>(request);
            context.Events.Add(Event);
            await context.SaveChangesAsync();
            return new ResponseModel<bool> { ResultCode = ResultCode.Success };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new ResponseModel<bool> { ResultCode = ResultCode.Failed, Message = e.Message };
        }
    }

    public async Task<ResponseModel<bool>> DeleteEvent(long id, string token)
    {
        try
        {
            await using var context = new ApplicationContext(_configuration);
            var Event = await context.Events.FindAsync(id);
            if (Event == null)
                return new ResponseModel<bool> { ResultCode = ResultCode.EventNotFound };
            context.Events.Remove(Event);
            await context.SaveChangesAsync();
            return new ResponseModel<bool> { ResultCode = ResultCode.Success };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new ResponseModel<bool> { ResultCode = ResultCode.Failed, Message = e.Message };
        }
    }

    public async Task<ResponseModel<PaginatedListModel<GetEventResponse>>> GetEvents(int page, int pageSize, GetEventsRequest request)
    {
        try
        {
            await using var context = new ApplicationContext(_configuration);
            var labaratories = await context.Events
            .Where(x => x.EventType == request.EventType &&
             (!string.IsNullOrEmpty(request.SearchString) && x.Title.Contains(request.SearchString)))
             .OrderByDescending(x => x.Id)
            .ToListAsync();

            var labaratoriesResponse = _mapper.Map<List<GetEventResponse>>(labaratories);
            var pagedLabaratories = PagedList<GetEventResponse>.ToPagedList(labaratoriesResponse, page, pageSize);

            var ans = _mapper.Map<PaginatedListModel<GetEventResponse>>(pagedLabaratories);
            return new ResponseModel<PaginatedListModel<GetEventResponse>>()
            { ResultCode = ResultCode.Success, Data = ans };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new ResponseModel<PaginatedListModel<GetEventResponse>> { ResultCode = ResultCode.Failed, Message = e.Message };
        }
    }

    public async Task<ResponseModel<GetEventResponse>> GetEventById(long Id)
    {
        try
        {
            await using var context = new ApplicationContext(_configuration);
            var Event = await context.Events.FirstOrDefaultAsync(x => x.Id == Id);
            if (Event == null)
                return new ResponseModel<GetEventResponse> { ResultCode = ResultCode.EventNotFound };

            var EventResponse = _mapper.Map<GetEventResponse>(Event);
            return new ResponseModel<GetEventResponse> { ResultCode = ResultCode.Success, Data = EventResponse };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new ResponseModel<GetEventResponse> { ResultCode = ResultCode.Failed, Message = e.Message };
        }
    }

    public async Task<ResponseModel<bool>> UpdateEvent(string token, UpdateEventRequest request)
    {
        try
        {
            await using var context = new ApplicationContext(_configuration);
            var Event = await context.Events.FindAsync(request.Id);
            if (Event == null)
                return new ResponseModel<bool> { ResultCode = ResultCode.EventNotFound };

            Event = _mapper.Map(request, Event);
            context.Events.Update(Event);
            await context.SaveChangesAsync();
            return new ResponseModel<bool> { ResultCode = ResultCode.Success };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new ResponseModel<bool> { ResultCode = ResultCode.Failed, Message = e.Message };
        }
    }
}
