using Enums;

namespace Responses;
public class GetEventResponse
{
    public long Id { get; set; }
    public EventType EventType { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string OwnerDescription { get; set; }
    public string Contacts { get; set; }
}
