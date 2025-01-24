using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuario.Domain.DTO;
using Usuario.Domain.Models;

namespace Usuario.Domain.Interfaces.Service
{
    public interface IUserService
    {
        Task<ResultData<bool>> AddUser(User newUser);
        Task<ResultData<User>> GetUserByUserId(int userId);
        Task<ResultData<bool>> UpdateUser(int id, User updatedUser);
        Task<ResultData<bool>> DeleteUser(int userId);
    }
}
