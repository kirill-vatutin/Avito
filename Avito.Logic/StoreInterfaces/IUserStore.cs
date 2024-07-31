using Avito.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avito.Logic.Stores
{
    public interface IUserStore
    {
        Task<IReadOnlyList<User>> Get();
       
        Task<User?> GetByEmail(string name);
        Task<User?> GetById(int id);
        Task Add(User user);

        Task Update(User user);
           
        Task Delete(User user);

        Task<int> GetRoleIdUser();

        bool VerifyUser(int tokenUserId, int userId);
        int GetUserIdFromJwt(string token);
        Task UpdateTelegramChatId(int userId, string chatId);
    }
}
