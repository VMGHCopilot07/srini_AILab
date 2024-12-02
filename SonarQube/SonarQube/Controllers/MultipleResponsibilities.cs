using System;
using System.Collections.Generic;
using System.IO;

public class UserManager
{
    private List<string> users = new List<string>();

    // Adds a user to the list
    public void AddUser(string user)
    {
        if (string.IsNullOrEmpty(user))
        {
            throw new ArgumentException("User cannot be null or empty");
        }
        users.Add(user);
    }

    // Removes a user from the list
    public void RemoveUser(string user)
    {
        if (string.IsNullOrEmpty(user))
        {
            throw new ArgumentException("User cannot be null or empty");
        }
        users.Remove(user);
    }

    // Retrieves all users
    public List<string> GetAllUsers()
    {
        return new List<string>(users);
    }

    // Logs a message to a file
    public void LogMessage(string message)
    {
        if (string.IsNullOrEmpty(message))
        {
            throw new ArgumentException("Message cannot be null or empty");
        }
        File.AppendAllText("log.txt", message + Environment.NewLine);
    }

    // Sends an email to a user
    public void SendEmail(string user, string subject, string body)
    {
        if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(body))
        {
            throw new ArgumentException("User, subject, and body cannot be null or empty");
        }
        Console.WriteLine($"Sending email to {user}: {subject} - {body}");
    }

    // Validates user input
    public bool ValidateUserInput(string user)
    {
        return !string.IsNullOrEmpty(user) && user.Length > 3;
    }
}

public class Program
{
    public static void Main()
    {
        var userManager = new UserManager();
        userManager.AddUser("JohnDoe");
        userManager.LogMessage("User JohnDoe added");
        userManager.SendEmail("JohnDoe", "Welcome", "Hello JohnDoe, welcome to our system!");
        bool isValid = userManager.ValidateUserInput("JohnDoe");
        Console.WriteLine($"Is valid user: {isValid}");
    }
}

