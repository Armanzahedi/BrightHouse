using SpadCompanyPanel.Core.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNet.Identity;
using SpadCompanyPanel.Infrastructure.Filters;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace SpadCompanyPanel.Infrastructure.Repositories
{
    public class UsersRepository
    {
        private readonly MyDbContext _context;
        private RoleStore<IdentityRole> _roleStore;
        private RoleManager<IdentityRole> _roleManager;
        private UserStore<User> _userStore;
        private UserManager<User> _userManager;
        public UsersRepository()
        {
            _context = new MyDbContext();
            _roleStore = new RoleStore<IdentityRole>(_context);
            _roleManager = new RoleManager<IdentityRole>(_roleStore);
            _userStore = new UserStore<User>(_context);
            _userManager = new UserManager<User>(_userStore);
        }
        public List<User> GetUsers()
        {
            var usersList = _userManager.Users.ToList();
            return usersList;
        }
        public List<User> GetUsersTable()
        {
            var usersList = _context.Users.Where(u => u.IsDeleted == false).Include(u => u.UserRoles).ToList();
            return usersList;
        }
        public User GetUser(string id)
        {
            var user = _context.Users.Find(id);
            return user;
        }
        public Tuple<User, bool> UpdateUser(User model, string newPassword = null)
        {
            var succeeded = true;
            var prevUser = _context.Users.Find(model.Id);
            _context.Entry(prevUser).State = EntityState.Detached;
            _context.Entry(model).State = EntityState.Modified;
            _context.SaveChanges();
            //_userManager.Update(model);
            if (!string.IsNullOrEmpty(newPassword))
            {
                var userPrevPassword = model.PasswordHash; // keeping prev password just in case setting new password fails
                var removePassword = _userManager.RemovePassword(model.Id);
                if (removePassword.Succeeded)
                {
                    var addPassword = _userManager.AddPassword(model.Id, newPassword);
                    if (addPassword.Succeeded == false)
                    {
                        succeeded = false;
                        model.PasswordHash = userPrevPassword;
                        _context.Entry(model).State = EntityState.Modified;
                        _context.SaveChanges();
                        //_userManager.Update(model);
                    }
                }
            }
            var updateModel = new Tuple<User, bool>(model, succeeded);
            return updateModel;
        }
        public User CreateUser(User model, string Password)
        {
            model.SecurityStamp = Guid.NewGuid().ToString();
            _userManager.Create(model, Password);

            return model;
        }
        public List<Role> GetRoles()
        {
            return _context.Role.ToList();
        }
        public bool UserHasRole(string userId, string roleId)
        {
            return _context.UserRoles.Any(a => a.UserId == userId && a.RoleId == roleId);
        }
        public UserRole AddUserRole(string userId, string roleId)
        {
            UserRole uRole = new UserRole()
            {
                UserId = userId,
                RoleId = roleId
            };
            _context.UserRoles.Add(uRole);
            _context.SaveChanges();
            return uRole;
        }
        public List<UserRole> GetUserRoles(string userId)
        {
            return _context.UserRoles.Where(ur => ur.UserId == userId).ToList();
        }
        public Role GetRole(string roleId)
        {
            return _context.Role.FirstOrDefault(r => r.Id == roleId);
        }
        public UserRole GetUserRole(string userId, string roleId)
        {
            return _context.UserRoles.FirstOrDefault(ur => ur.UserId == userId && ur.RoleId == roleId);
        }
        public User DeleteUser(string userId)
        {
            var user = _context.Users.Find(userId);
            //var userRoles = _context.UserRoles.Where(ur => ur.UserId == userId);
            //foreach (var userRole in userRoles)
            //{
            //    _context.UserRoles.Remove(userRole);
            //}

            user.IsDeleted = true;
            _context.Users.Remove(user);
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
            return user;
        }
        public UserRole DeleteUserRole(UserRole userRole)
        {
            _context.UserRoles.Remove(userRole);
            _context.SaveChanges();
            return userRole;
        }
        public User FindByPhone(string phone)
        {
            return _context.Users.FirstOrDefault(u => u.PhoneNumber.ToLower().Trim() == phone.Trim().ToLower() && u.IsDeleted == false);
        }
        public User FindByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email.ToLower().Trim() == email.Trim().ToLower() && u.IsDeleted == false);
        }
        public bool UserNameExists(string username, string id = null)
        {
            var user = _userManager.FindByName(username);
            if (user == null || user.IsDeleted) return false;

            if (string.IsNullOrEmpty(id))
                return true;

            if (user.Id != id)
                return true;

            return false;
        }
        public bool PhoneNumberExists(string phoneNumber, string id = null)
        {
            var user = _context.Users.FirstOrDefault(u => u.PhoneNumber == phoneNumber);
            if (user == null || user.IsDeleted) return false;

            if (string.IsNullOrEmpty(id))
                return true;

            if (user.Id != id)
                return true;

            return false;
        }
        public Customer GetCustomerByUserId(string userId)
        {
            return _context.Customers.FirstOrDefault(c => c.UserId == userId);
        }
        public Customer AddCustomer(Customer model)
        {
            _context.Customers.Add(model);
            _context.SaveChanges();
            return model;
        }
        public async Task<IdentityResult> ValidatePassword(string password)
        {
            return await _userManager.PasswordValidator.ValidateAsync(password);
        }
        public async Task<IdentityResult> SetNewPassword(string userId, string oldPassword, string newPassword)
        {
            var result = await _userManager.ChangePasswordAsync(userId, oldPassword, newPassword);
            return result;
        }
        public async Task<IdentityResult> ResetPasswordToDefault(string userId)
        {
            var appSettings = ConfigurationManager.AppSettings;
            await _userManager.RemovePasswordAsync(userId);
            var result = await _userManager.AddPasswordAsync(userId, appSettings["UserDefaultPassword"]);
            return result;
        }
        public bool EmailExists(string email, string id = null)
        {
            var user = _userManager.FindByEmail(email);
            if (user == null || user.IsDeleted) return false;

            if (string.IsNullOrEmpty(id))
                return true;

            if (user.Id != id)
                return true;

            return false;
        }
        protected void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public void Dispose()
        {
            _context?.Dispose();
            _roleStore?.Dispose();
            _roleManager?.Dispose();
            _userStore?.Dispose();
            _userManager?.Dispose();
        }
    }
}
