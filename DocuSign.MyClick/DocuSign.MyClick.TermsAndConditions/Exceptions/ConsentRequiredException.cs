using System;

namespace DocuSign.MyClick.TermsAndConditions.Exceptions
{
    public class ConsentRequiredException : Exception
    {
        public ConsentRequiredException()
            : base("JWT token cannot be granted without consent.")
        {
        }
    }
}