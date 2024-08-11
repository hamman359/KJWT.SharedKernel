namespace KJWT.SharedKernel.Results;

public interface IResult
{
    ResultStatus Status { get; }
    IEnumerable<Error> Errors { get; }
    IEnumerable<ValidationError> ValidationErrors { get; }
    Type ValueType { get; }
    object GetValue();
}
