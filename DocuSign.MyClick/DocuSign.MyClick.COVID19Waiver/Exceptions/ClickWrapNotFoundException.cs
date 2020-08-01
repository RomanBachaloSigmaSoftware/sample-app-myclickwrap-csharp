﻿using System;

namespace DocuSign.MyClick.COVID19Waiver.Exceptions
{
    public class ClickWrapNotFoundException : Exception
    {
        public ClickWrapNotFoundException(string clickWrapName)
            : base(
                $"Click wrap {clickWrapName} is not found in preconfigured click wraps.")
        {
        }
    }
}