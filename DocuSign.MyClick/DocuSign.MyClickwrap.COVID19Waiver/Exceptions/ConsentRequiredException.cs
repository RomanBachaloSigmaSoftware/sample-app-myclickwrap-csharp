using System;

namespace DocuSign.MyClickwrap.COVID19Waiver.Exceptions
{
    public class ConsentRequiredException : Exception
    {
        public ConsentRequiredException()
            : base(
                "JWT token cannot be granted without consent.")
        {
        }
    }
}