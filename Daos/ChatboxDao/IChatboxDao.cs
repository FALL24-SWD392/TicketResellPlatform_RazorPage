using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daos.ChatboxDao
{
    public interface IChatboxDao : IDao<Chatbox>
    {
        Task<List<Chatbox>> GetChatBoxOfUser(int userId);
    }
}
