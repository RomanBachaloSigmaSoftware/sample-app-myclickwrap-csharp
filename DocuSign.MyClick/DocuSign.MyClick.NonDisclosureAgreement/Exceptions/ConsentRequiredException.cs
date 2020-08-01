﻿using System;

namespace DocuSign.MyClick.NonDisclosureAgreement.Exceptions
{
    public class ConsentRequiredException : Exception
    {
        public ConsentRequiredException()
            : base("JWT token cannot be granted without consent.")
        {
        }
    }
}