using System;

namespace GroupProject.Models;

[Flags]
enum ErrorType
{
    InvalidUsername,
    InvalidPassword,
    InvalidEmail,
    ExistingUser
}