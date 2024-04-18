using Entities;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositorio
{
    public interface IUserRepository
    {
        ICollection<User> GetAllUSer();
        User GetUserById(int id);
        bool IsUniqueUser(string userName);
        UserLoginResponseDto Login(UserLoginDto userLogin);
    }
}
