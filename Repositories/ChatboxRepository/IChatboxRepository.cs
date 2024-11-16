using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.ChatboxRepository
{
    public interface IChatboxRepository : IRepository<Chatbox>
    {
        Task<List<Chatbox>> GetChatBoxOfUser(int UserId);
    }
}
