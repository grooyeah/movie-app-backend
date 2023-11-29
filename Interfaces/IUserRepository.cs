﻿using Models;

namespace Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(string userId);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}