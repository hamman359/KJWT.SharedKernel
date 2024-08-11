namespace KJWT.SharedKernel.Results;

public record ErrorList(IEnumerable<Error> Errors, string? CorrelationId = null);