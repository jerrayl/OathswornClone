using System;

namespace Oathsworn.Infrastructure
{
    public class ErrorMessageException (string errorMessage): Exception (errorMessage)
    {
        // Use to throw bad request error messages to display to the user
    }
}