using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Restaurant_Model;
using Restaurant_Model.Request;
using Restaurant_Model.SearchObjects;
using Restaurant_Services.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Services
{
    public class UserService: BaseCRUDService<Restaurant_Model.Users, Database.Users, UserSearchObject, UserInsertRequest, UserUpdateRequest>, IUserService
    {
        public UserService(RestaurantDbContext context, IMapper mapper): base(context, mapper) { }

        public override Restaurant_Model.Users Insert(UserInsertRequest insert)
        {
            var entity = base.Insert(insert);

            foreach (var roleId in insert.RolesIdList)
            {
                Database.UserRole role = new Database.UserRole();
                role.RoleId = roleId;
                role.UserId = entity.UserId;

                context.UserRoles.Add(role);
            }

            context.SaveChanges();
            return entity;
        }

        public override void BeforeInsert(UserInsertRequest insert, Database.Users entity)
        {
            var salt = GenerateSalt();
            entity.PasswordSalt = salt;
            entity.PasswordHash = GenerateHash(salt, insert.Password);
            base.BeforeInsert(insert, entity);
        }

        public static string GenerateSalt()
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            var byteArray = new byte[16];
            provider.GetBytes(byteArray);


            return Convert.ToBase64String(byteArray);
        }
        static string GenerateHash(string salt, string password)
        {
            try
            {
                byte[] src = Convert.FromBase64String(salt);
                byte[] bytes = Encoding.Unicode.GetBytes(password);
                byte[] dst = new byte[src.Length + bytes.Length];

                System.Buffer.BlockCopy(src, 0, dst, 0, src.Length);
                System.Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);

                HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
                byte[] inArray = algorithm.ComputeHash(dst);
                return Convert.ToBase64String(inArray);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public Restaurant_Model.Users Login(string username, string password)
        {

            var entity = context.Users.Include("UserRoles.Role").FirstOrDefault(x => x.UserName == username);
            if (entity == null)
            {
                return null;
            }

            var hash = GenerateHash(entity.PasswordSalt, password);
            if (hash != entity.PasswordHash)
            {
                return null;
            }

            return mapper.Map<Restaurant_Model.Users>(entity);
        }

    }
}
