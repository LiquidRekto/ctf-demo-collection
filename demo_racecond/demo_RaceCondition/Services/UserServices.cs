using demo_RaceCondition.Models;

public class UserService
{
    private readonly demo_bypassContext _context;

    private Acccount? _currentUser;

    public UserService(demo_bypassContext context)
    {
        _context = context;
    }

    public IEnumerable<Acccount> GetAllUsers()
    {
        return _context.Acccounts.ToList();
    }

    public Acccount? GetUserById(int id)
    {
        // Find a user by their ID
        var account = _context.Acccounts.FirstOrDefault(a => a.Id == id);

        return account;
    }

    public void UpdateEmail(int id, string newEmail)
    {
        // Update user email in the database
        var user = GetUserById(id);
        if (user != null)
        {
            user.Email = newEmail;
            user.IsComfirmEmail = false; // Reset email confirmation status
            _context.SaveChanges();
        }
    }

    public void ConfirmEmail(int id)
    {
        // Confirm user's email
        var user = GetUserById(id);
        if (user != null)
        {
            user.IsComfirmEmail = true; // Set email as confirmed
            _context.SaveChanges();
        }
    }

    public void DeleteUser(int id)
    {
        // Delete user from the database
        var user = GetUserById(id);
        if (user != null)
        {
            _context.Acccounts.Remove(user);
            _context.SaveChanges();
        }
    }

    public Acccount? Login(string email, string password)
    {
        // Authenticate user by email and password
        var account = _context.Acccounts.FirstOrDefault(u => u.Email == email && u.Password == password);
        if (account != null)
        {
            _currentUser = new Acccount
            {
                Id = account.Id,
                Name = account.Name,
                Email = account.Email,
                Role = account.Role,
                IsComfirmEmail = account.IsComfirmEmail // Reflect confirmation status on login
            };
        }
        return _currentUser;
    }

    public Acccount? GetCurrentUser() => _currentUser;
}
