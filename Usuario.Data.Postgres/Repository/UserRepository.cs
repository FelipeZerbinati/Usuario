using Usuario.Domain.Interfaces.Rest;
using Usuario.Domain.DTO;
using Usuario.Domain.Models;
using RestSharp;
using Usuario.Data.Rest.Repository;
using Usuario.Data.Postgres.Context;
using Microsoft.EntityFrameworkCore;
using Usuario.Domain.Interfaces.Rest;

namespace Usuario.Data.Postgres.UserRepository
{


    public class UserRepository : IUserRepository
    {
        private readonly UsuarioContext _context;

        public UserRepository(UsuarioContext context)
        {
            _context = context;
        }

        public async Task<ResultData<bool>> AddUser(User newUser)
        {
            try
            {
                await _context.User.AddAsync(newUser);
                await _context.SaveChangesAsync();
                return new ResultData<bool> { Success = true, Data = true };
            }
            catch (Exception ex)
            {
                return new ResultData<bool> { Success = false, ErrorDescription = ex.Message };
            }
        }

        public async Task<ResultData<User>> GetUserByUserId(int userId)
        {
            try
            {
                var user = await _context.User.FindAsync(userId);
                if (user == null)
                {
                    return new ResultData<User> { Success = false, ErrorDescription = "User not found." };
                }

                return new ResultData<User> { Success = true, Data = user };
            }
            catch (Exception ex)
            {
                return new ResultData<User> { Success = false, ErrorDescription = ex.Message };
            }
        }

        public async Task<ResultData<bool>> UpdateUser(User updatedUser)
        {
            try
            {
                var exists = await _context.User.AnyAsync(u => u.Id == updatedUser.Id);
                if (!exists)
                {
                    return new ResultData<bool> { Success = false, ErrorDescription = "User not found." };
                }

                _context.User.Update(updatedUser);
                await _context.SaveChangesAsync();
                return new ResultData<bool> { Success = true, Data = true };
            }
            catch (Exception ex)
            {
                return new ResultData<bool> { Success = false, ErrorDescription = ex.Message };
            }
        }

        public async Task<ResultData<bool>> DeleteUser(int userId)
        {
            try
            {
                var user = await _context.User.FindAsync(userId);
                if (user == null)
                {
                    return new ResultData<bool> { Success = false, ErrorDescription = "User not found." };
                }

                _context.User.Remove(user);
                await _context.SaveChangesAsync();
                return new ResultData<bool> { Success = true, Data = true };
            }
            catch (Exception ex)
            {
                return new ResultData<bool> { Success = false, ErrorDescription = ex.Message };
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.User.AnyAsync(u => u.Id == id);
        }

    }



}
