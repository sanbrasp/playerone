using System.Net.Mail;

namespace PlayerOne.Helpers;

internal static class ValidationHelpers
{
    // /// <summary>
    // /// Performs basic validation of a card number string.
    // /// Extracts digits and checks whether the resulting length matches
    // /// common card number lengths (13–16 or 19 digits).
    // /// </summary>
    // /// <param name="prompt">The raw card number input.</param>
    // /// <returns>
    // /// <c>true</c> if the card number contains a valid number of digits;
    // /// otherwise <c>false</c>.
    // /// </returns>
    // public static bool ValidateCardNumber(string prompt)
    // {
    //     if (string.IsNullOrWhiteSpace(prompt))
    //         return false;
    //     
    //     // Normalization because users write strange things:
    //     string digitsOnly = new string(prompt.Where(char.IsDigit).ToArray());
    //     
    //     int len = digitsOnly.Length;
    //     bool validLength = len == 13 || len == 14 || len == 15 || len == 16 || len == 19; 
    //     
    //     return validLength;
    // }
    
    // /// <summary>
    // /// Validates whether the provided string is an 8‑digit phone number.
    // /// Accepts only numeric characters.
    // /// </summary>
    // /// <param name="s">The phone number string to validate.</param>
    // /// <returns>
    // /// <c>true</c> if the string contains exactly 8 digits; otherwise <c>false</c>.
    // /// </returns>
    // public static bool IsEightDigitPhoneNumber(string? s)
    // => !string.IsNullOrWhiteSpace(s) && s.All(char.IsDigit) && s.Length == 8;
    //
    // /// <summary>
    // /// Extracts and returns only the digit characters from the provided string.
    // /// Null-safe: returns an empty string if input is null.
    // /// </summary>
    // /// <param name="s">The input string.</param>
    // /// <returns>A string containing only digit characters.</returns>
    // public static string DigitsOnly(string? s)
    // => new string((s ?? string.Empty).Where(char.IsDigit).ToArray());

    // /// <summary>
    // /// Validates an email address using <see cref="MailAddress"/> parsing
    // /// and additional domain and TLD checks.
    // /// Ensures no whitespace, no double dots, a valid '@' position,
    // /// and a top-level domain of at least two characters.
    // /// </summary>
    // /// <param name="email">The email address to validate.</param>
    // /// <returns>
    // /// <c>true</c> if the email address is syntactically valid; otherwise <c>false</c>.
    // /// </returns>
    // public static bool IsValidEmail(string? email)
    // {
    //     if (string.IsNullOrWhiteSpace(email))
    //         return false;
    //
    //     email = email.Trim();
    //
    //     try
    //     {
    //         var addr = new MailAddress(email);
    //         if (!string.Equals(addr.Address, email, StringComparison.Ordinal))
    //             return false;
    //     }
    //     catch (Exception _)
    //     {
    //         return false;
    //     }
    //
    //     int atIndex = email.LastIndexOf('@');
    //     if (atIndex <= 0 || atIndex == email.Length - 1)
    //         return false;
    //
    //     string domain = email[(atIndex + 1)..];
    //
    //     if (domain.Contains(' ') || domain.Contains(".."))
    //         return false;
    //
    //     int lastDot = domain.LastIndexOf('.');
    //     if (lastDot <= 0 || lastDot == domain.Length - 1)
    //         return false;
    //
    //     string tld = domain[(lastDot + 1)..];
    //     if (tld.Length < 2)
    //         return false;
    //     
    //     return true;
    // }
}