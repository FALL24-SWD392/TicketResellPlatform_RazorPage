using Business;
using Daos.TicketDao;
using Repositories.TicketRepository;
using Repositories.TicketTypeRepository;

namespace Services.TicketService
{
    public class TicketService : ITicketService
    {
        private static TicketService? instance;
        private readonly ITicketRepository ticketRepo;
        private readonly ITicketTypeRepository ticketTypeRepo;
        private TicketService()
        {
            ticketRepo = TicketRepository.Instance;
            ticketTypeRepo = TicketTypeRepository.Instance;
        }

        public async Task<Ticket?> AddAsync(Ticket entity)
            => await ticketRepo.AddAsync(entity);

        public async Task<Ticket?> DeleteAsync(int id)
             => await ticketRepo.DeleteAsync(id);

        public async Task<IList<Ticket>> GetAllAsync()
            => await ticketRepo.GetAllAsync();

        public async Task<IList<TicketType>> GetAllTicketType()
            => await ticketTypeRepo.GetAllAsync();

        public async Task<Ticket?> GetAsync(int id)
            => await ticketRepo.GetAsync(id);

        public async Task<Ticket?> UpdateAsync(Ticket entity)
            => await ticketRepo.UpdateAsync(entity);
    }
}
