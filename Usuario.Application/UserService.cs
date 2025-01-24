using Usuario.Domain.DTO;
using Usuario.Domain.Interfaces.Rest;
using Usuario.Domain.Interfaces.Service;
using Usuario.Domain.Models;

namespace Usuario.Application
{
    

    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultData<bool>> AddUser(User newUser)
        {
            var result = new ResultData<bool>();
            try
            {
                if (newUser == null)
                {
                    result.Success = false;
                    result.ErrorDescription = "User cannot be null.";
                    return result;
                }

                await _repository.AddUser(newUser);
                result.Success = true;
                result.Data = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorDescription = $"Erro inesperado: {ex.Message}";
            }
            finally
            {
                if (!result.Success)
                {
                    result.Data = false;
                }
            }

            return result;
        }

        public async Task<ResultData<User>> GetUserByUserId(int userId)
        {
            var result = new ResultData<User>();
            try
            {
                var userResult = await _repository.GetUserByUserId(userId);

                if (userResult == null)
                {
                    result.Success = false;
                    result.ErrorDescription = "User not found.";
                }
                else
                {
                    result.Success = true;
                    result.Data = userResult.Data;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorDescription = $"Erro inesperado: {ex.Message}";
            }
            finally
            {
                if (!result.Success)
                {
                    result.Data = new User();
                }
            }

            return result;
        }

        public async Task<ResultData<bool>> UpdateUser(int id, User updatedUser)
        {
            var result = new ResultData<bool>();
            try
            {
                var exists = await _repository.ExistsAsync(id);
                if (!exists)
                {
                    result.Success = false;
                    result.ErrorDescription = "User does not exist.";
                    return result;
                }

                await _repository.UpdateUser(updatedUser);
                result.Success = true;
                result.Data = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorDescription = $"Erro inesperado: {ex.Message}";
            }
            finally
            {
                if (!result.Success)
                {
                    result.Data = false;
                }
            }

            return result;
        }

        public async Task<ResultData<bool>> DeleteUser(int userId)
        {
            var result = new ResultData<bool>();
            try
            {
                var exists = await _repository.ExistsAsync(userId);
                if (!exists)
                {
                    result.Success = false;
                    result.ErrorDescription = "User does not exist.";
                    return result;
                }

                await _repository.DeleteUser(userId);
                result.Success = true;
                result.Data = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorDescription = $"Erro inesperado: {ex.Message}";
            }
            finally
            {
                if (!result.Success)
                {
                    result.Data = false;
                }
            }

            return result;
        }
    }
}
