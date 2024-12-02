using System;
using System.Collections.Generic;

public class UserManager
{
    private readonly ILogger _logger;
    private readonly IEmailService _emailService;
    private readonly IUserRepository _userRepository;
    private readonly INotificationService _notificationService;
    private readonly IAuditService _auditService;

    public UserManager(ILogger logger, IEmailService emailService, IUserRepository userRepository, INotificationService notificationService, IAuditService auditService)
    {
        _logger = logger;
        _emailService = emailService;
        _userRepository = userRepository;
        _notificationService = notificationService;
        _auditService = auditService;
    }

    public void AddUser(string user)
    {
        ValidateUser(user);

        _userRepository.AddUser(user);
        LogAndNotify(user, "added", "Welcome", $"Hello {user}, welcome to our system!");
        _auditService.RecordAction("AddUser", user);
    }

    public void RemoveUser(string user)
    {
        ValidateUser(user);

        _userRepository.RemoveUser(user);
        LogAndNotify(user, "removed", null, null);
        _auditService.RecordAction("RemoveUser", user);
    }

    public List<string> GetAllUsers()
    {
        return _userRepository.GetAllUsers();
    }

    private void ValidateUser(string user)
    {
        if (string.IsNullOrEmpty(user))
        {
            throw new ArgumentException("User cannot be null or empty");
        }
    }

    private void LogAndNotify(string user, string action, string emailSubject, string emailBody)
    {
        _logger.LogMessage($"User {user} {action}");
        if (!string.IsNullOrEmpty(emailSubject) && !string.IsNullOrEmpty(emailBody))
        {
            _emailService.SendEmail(user, emailSubject, emailBody);
        }
        _notificationService.Notify(user, $"You have been {action} to/from the system.");
    }
}

public interface ILogger
{
    void LogMessage(string message);
}

public class FileLogger : ILogger
{
    public void LogMessage(string message)
    {
        if (string.IsNullOrEmpty(message))
        {
            throw new ArgumentException("Message cannot be null or empty");
        }
        File.AppendAllText("log.txt", message + Environment.NewLine);
    }
}

public interface IEmailService
{
    void SendEmail(string user, string subject, string body);
}

public class EmailService : IEmailService
{
    public void SendEmail(string user, string subject, string body)
    {
        if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(body))
        {
            throw new ArgumentException("User, subject, and body cannot be null or empty");
        }
        Console.WriteLine($"Sending email to {user}: {subject} - {body}");
    }
}

public interface IUserRepository
{
    void AddUser(string user);
    void RemoveUser(string user);
    List<string> GetAllUsers();
}

public class UserRepository : IUserRepository
{
    private List<string> users = new List<string>();

    public void AddUser(string user)
    {
        users.Add(user);
    }

    public void RemoveUser(string user)
    {
        users.Remove(user);
    }

    public List<string> GetAllUsers()
    {
        return new List<string>(users);
    }
}

public interface INotificationService
{
    void Notify(string user, string message);
}

public class NotificationService : INotificationService
{
    public void Notify(string user, string message)
    {
        if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(message))
        {
            throw new ArgumentException("User and message cannot be null or empty");
        }
        Console.WriteLine($"Notifying {user}: {message}");
    }
}

public interface IAuditService
{
    void RecordAction(string action, string user);
}

public class AuditService : IAuditService
{
    public void RecordAction(string action, string user)
    {
        if (string.IsNullOrEmpty(action) || string.IsNullOrEmpty(user))
        {
            throw new ArgumentException("Action and user cannot be null or empty");
        }
        Console.WriteLine($"Recording action {action} for user {user}");
    }
}
















