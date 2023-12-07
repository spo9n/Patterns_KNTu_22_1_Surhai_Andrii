namespace Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public int UserRoleId { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }


        private User()
        {

        }

        public class Builder
        {
            private User user = new User();

            public Builder WithId(int id)
            {
                user.Id = id;
                return this;
            }

            public Builder WithUserRoleId(int userRoleid)
            {
                user.UserRoleId = userRoleid;
                return this;
            }

            public Builder WithSurname(string surname)
            {
                user.Surname = surname;
                return this;
            }

            public Builder WithName(string name)
            {
                user.Name = name;
                return this;
            }

            public Builder WithPatronymic(string patronymic)
            {
                user.Patronymic = patronymic;
                return this;
            }

            public Builder WithEmail(string email)
            {
                user.Email = email;
                return this;
            }

            public Builder WithPhone(string phone)
            {
                user.Phone = phone;
                return this;
            }

            public Builder WithUsername(string username)
            {
                user.Username = username;
                return this;
            }

            public Builder WithPassword(string password)
            {
                user.Password = password;
                return this;
            }

            public User Build()
            {
                if (user.UserRoleId == null || string.IsNullOrEmpty(user.Surname) || string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Patronymic) ||
                    string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Phone) || string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
                {
                    throw new InvalidOperationException("Fields are required.");
                }
                return user;
            }
        }
    }
}
