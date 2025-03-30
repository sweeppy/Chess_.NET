using System.Text.RegularExpressions;

public static class PasswordValidator
{
    public static (bool IsValid, string Message) ValidatePassword(string password, string confirmPassword)
    {
        if (password != confirmPassword)
        {
            return (false, "Passwords do not match.");
        }

        // Minimum password length
        if (password.Length < 8)
        {
            return (false, "Password must be at least 8 characters long.");
        }

        // Check for at least one digit
        if (!Regex.IsMatch(password, @"\d"))
        {
            return (false, "Password must contain at least one digit.");
        }

        // Check for at least one uppercase letter
        if (!Regex.IsMatch(password, @"[A-Z]"))
        {
            return (false, "Password must contain at least one uppercase letter.");
        }

        // Check for at least one lowercase letter
        if (!Regex.IsMatch(password, @"[a-z]"))
        {
            return (false, "Password must contain at least one lowercase letter.");
        }

        // Check for at least one special character
        if (!Regex.IsMatch(password, @"[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]"))
        {
            return (false, "Password must contain at least one special character.");
        }

        // If all checks pass
        return (true, "Password is valid.");
    }
    public static (bool IsValid, string Message) ValidatePassword(string password)
    {
        // Minimum password length
            if (password.Length < 8)
            {
                return (false, "Password must be at least 8 characters long.");
            }

        // Check for at least one digit
        if (!Regex.IsMatch(password, @"\d"))
        {
            return (false, "Password must contain at least one digit.");
        }

        // Check for at least one uppercase letter
        if (!Regex.IsMatch(password, @"[A-Z]"))
        {
            return (false, "Password must contain at least one uppercase letter.");
        }

        // Check for at least one lowercase letter
        if (!Regex.IsMatch(password, @"[a-z]"))
        {
            return (false, "Password must contain at least one lowercase letter.");
        }

        // Check for at least one special character
        if (!Regex.IsMatch(password, @"[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]"))
        {
            return (false, "Password must contain at least one special character.");
        }

        // If all checks pass
        return (true, "Password is valid.");
    }
}