namespace Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities
{
    public class UserRole
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Guest";


        private UserRole()
        {

        }

        public class Builder
        {
            private UserRole _userRole = new UserRole();

            public Builder WithId(int id)
            {
                _userRole.Id = id;
                return this;
            }

            public Builder WithName(string name)
            {
                _userRole.Name = name;
                return this;
            }

            public UserRole Build()
            {
                if (string.IsNullOrEmpty(_userRole.Name))
                {
                    throw new InvalidOperationException("Field Name is required.");
                }
                return _userRole;
            }
        }
    }
}
