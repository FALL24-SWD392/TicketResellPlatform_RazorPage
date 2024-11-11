using Business;

namespace Services.TicketService
{
    public interface ITicketService : IService<Ticket>
    {
        Task<IList<TicketType>> GetAllTicketType();
    }
}
