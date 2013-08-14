using RecipeData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeModels;
namespace RecipeRepository
{
    public class UserRepository : IRepository<User>
    {
        private const string SessionKeyChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        private const int SessionKeyLen = 50;
        private const int MinUsernameNicknameChars = 4;
        private const int MaxUsernameNicknameChars = 30;

        public UserRepository(RecipeContext context)
        {
        }

        private static string GenerateSessionKey(int userId)
        {
            Random rand = new Random();
            StringBuilder keyChars = new StringBuilder(50);
            keyChars.Append(userId.ToString());
            while (keyChars.Length < SessionKeyLen)
            {
                int randomCharNum;
                randomCharNum = rand.Next(SessionKeyChars.Length);
                char randomKeyChar = SessionKeyChars[randomCharNum];
                keyChars.Append(randomKeyChar);
            }
            string sessionKey = keyChars.ToString();
            return sessionKey;
        }

        private static void ValidateUsername(string username)
        {
            if (username == null || username.Length < MinUsernameNicknameChars || username.Length > MaxUsernameNicknameChars)
            {
                throw new ArgumentException("Username should be between 4 and 30 symbols long", "INV_USRNAME_LEN");
            }
        }

        public  void CreateUser(string username,  string authCode)
        {
            ValidateUsername(username);

            using (RecipeContext context = new RecipeContext())
            {
                var usernameToLower = username.ToLower();

                var dbUser = context.Users.FirstOrDefault(u => u.UserName == username);

                if (dbUser != null)
                {
                    if (dbUser.UserName.ToLower() == usernameToLower)
                    {
                        throw new ArgumentException("Username already exists", "ERR_DUP_USR");
                    }
                    else
                    {
                        throw new ArgumentException("Nickname already exists", "ERR_DUP_NICK");
                    }
                }

                dbUser = new User()
                {
                    UserName = usernameToLower,
                    
                    Password = authCode
                };

                context.Users.Add(dbUser);
                context.SaveChanges();
            }

        }

        public User LoginUser(string username, string authCode)
        {
            ValidateUsername(username);
            var context = new RecipeContext();
            using (context)
            {
                var usernameToLower = username.ToLower();
                var user = context.Users.FirstOrDefault(u => u.UserName.ToLower() == usernameToLower && u.Password == authCode);
                if (user == null)
                {
                    throw new ArgumentException("Invalid user authentication", "INV_USR_AUTH");
                }

                var sessionKey = GenerateSessionKey((int)user.UserId);
                user.SessionKey = sessionKey;
                context.SaveChanges();
                return user;
            }
        }

        public  int LoginUser(string sessionKey)
        {
            
            var context = new RecipeContext();
            using (context)
            {
                var user = context.Users.FirstOrDefault(u => u.SessionKey == sessionKey);
                if (user == null)
                {
                    throw new ArgumentException("Invalid user authentication", "INV_USR_AUTH");
                }
                return (int)user.UserId;
            }
        }

        public  void LogoutUser(string sessionKey)
        {

            var context = new RecipeContext();
            using (context)
            {
                var user = context.Users.FirstOrDefault(u => u.SessionKey == sessionKey);
                if (user == null)
                {
                    throw new ArgumentException("Invalid user authentication", "INV_USR_AUTH");
                }

                user.SessionKey = null;
                context.SaveChanges();
            }
        }

        public IQueryable<User> All()
        {
            var context = new RecipeContext();
            return context.Users;
        }

        public User Get(int id)
        {
            var context = new RecipeContext();
            return context.Users.Find(id);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public User Add(User item)
        {
            throw new NotImplementedException();
        }

        public User Update(int id, User item)
        {
            throw new NotImplementedException();
        }

        public void Delete(User item)
        {
            throw new NotImplementedException();
        }
    }
}
