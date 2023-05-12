using Rectangle.WebApi.JWT.Models;

namespace Rectangle.WebApi.JWT
{
    public class UserConstants
    {
        public static List<UserModel> Users = new()
        {
            new UserModel(){ Username="admin",Password="testAdmin",Role="Admin"},
            new UserModel(){ Username="user",Password="testUser",Role="User"}
        };
    }
}
