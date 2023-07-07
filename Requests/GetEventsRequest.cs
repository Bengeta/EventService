using Enums;

namespace Requests;
public class GetEventsRequest
{
    public EventType EventType { get; set; }
    public string SearchString { get; set; } = "";
}