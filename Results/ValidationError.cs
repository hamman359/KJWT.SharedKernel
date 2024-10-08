﻿using System.ComponentModel.DataAnnotations;

namespace KJWT.SharedKernel.Results;

public class ValidationError
{
    public ValidationError()
    {
    }

    public ValidationError(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }

    public ValidationError(string identifier, string errorMessage, string errorCode, ValidationSeverity severity)
    {
        Identifier = identifier;
        ErrorMessage = errorMessage;
        ErrorCode = errorCode;
        Severity = severity;
    }

    public string Identifier { get; set; } = string.Empty;

    [Required]
    public string ErrorMessage { get; set; } = string.Empty;

    public string ErrorCode { get; set; } = string.Empty;

    public ValidationSeverity Severity { get; set; } = ValidationSeverity.Error;
}