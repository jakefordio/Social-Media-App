namespace SocialMediaApp.API.Models{
//     IMPORTANT! Anytime a Model class is created, or properties of the class edited,
//     We need to create a new migration for the database via Terminal with following commands:
//      1. dotnet ef migrations add AddedUserEntity <-- This creates the new migration for your entity
//      2. After that double check newly created migration to make sure columns were created correctly.
//      3. dotnet ef database update <-- Executes and creates the new table in the database.
    public class User
    {

        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash{ get; set; }
        public byte[] PasswordSalt{ get; set; }

    }

}