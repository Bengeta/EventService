using Analysis.Models;
using AutoMapper;
using Models.DBTables;
using Requests;
using Responses;

namespace Utils
{
    public class AutoMappingProfiles : Profile
    {
        public AutoMappingProfiles()
        {
            CreateMap<EventModel, GetEventResponse>();
            CreateMap<CreateEventRequest, EventModel>();
            CreateMap<UpdateEventRequest, EventModel>();

            CreateMap(typeof(PagedList<>), typeof(PaginatedListModel<>))
                .ConvertUsing(typeof(PagedListTypeConverter<>));
        }
    }
}