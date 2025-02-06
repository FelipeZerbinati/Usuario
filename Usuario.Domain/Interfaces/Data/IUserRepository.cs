using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuario.Domain.DTO;
using Usuario.Domain.Models;

namespace Usuario.Domain.Interfaces.Data
{
    public interface IUserRepository
    {
        Task<ResultData<bool>> AddUser(User newUser);
        Task<ResultData<User>> GetUserByUserId(int userId);
        Task<ResultData<bool>> UpdateUser(User updatedUser);
        Task<ResultData<bool>> DeleteUser(int userId);
        Task<bool> ExistsAsync(int id);
    }
}
