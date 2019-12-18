﻿using Kibol_Alert.Database;
using Kibol_Alert.Models;
using Kibol_Alert.Requests;
using Kibol_Alert.Responses;
using Kibol_Alert.Services.Interfaces;
using Kibol_Alert.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kibol_Alert.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(Kibol_AlertContext context) : base(context)
        {
        }

        public async Task<Response> BanUser(string id)
        {
            var user = await Context.Users.FirstOrDefaultAsync(i => i.Id == id && i.IsBanned == false);
            if (user == null)
            {
                return new ErrorResponse("User not found or already banned!");
            }
            user.IsBanned = true;
            Context.Users.Update(user);
            await Context.SaveChangesAsync();
            return new SuccessResponse<bool>(true);
        }

        public async Task<Response> UnbanUser(string id)
        {
            var user = await Context.Users.FirstOrDefaultAsync(i => i.Id == id && i.IsBanned == true);
            if (user == null)
            {
                return new ErrorResponse("User not found or already unbanned!");
            }
            user.IsBanned = false;
            Context.Users.Update(user);
            await Context.SaveChangesAsync();
            return new SuccessResponse<bool>(true);
        }

        public async Task<Response> DeleteUser(string id)
        {
            var user = await Context.Users.FirstOrDefaultAsync(i => i.Id == id && i.IsDeleted == false);
            if (user == null)
            {
                return new ErrorResponse("User not found or already deleted!");
            }
            user.IsDeleted = true;
            Context.Users.Update(user);
            await Context.SaveChangesAsync();
            return new SuccessResponse<bool>(true);
        }

        public async Task<Response> GiveAdmin(string id)
        {
            var user = await Context.Users.FirstOrDefaultAsync(i => i.Id == id && i.IsAdmin == false);
            if (user == null)
            {
                return new ErrorResponse("User not found or alread is admin!");
            }
            user.IsDeleted = true;
            Context.Users.Update(user);
            await Context.SaveChangesAsync();
            return new SuccessResponse<bool>(true);
        }

        public async Task<Response> TakeAdmin(string id)
        {
            var user = await Context.Users.FirstOrDefaultAsync(i => i.Id == id && i.IsAdmin == true);
            if (user == null)
            {
                return new ErrorResponse("User not found or alread isn't admin!");
            }
            user.IsDeleted = false;
            Context.Users.Update(user);
            await Context.SaveChangesAsync();
            return new SuccessResponse<bool>(true);
        }

        public Task<Response> EditUser(string id, UserRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Response> GetUser(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Response> GetUsers(int skip, int take)
        {
            throw new NotImplementedException();
        }
    }
}
