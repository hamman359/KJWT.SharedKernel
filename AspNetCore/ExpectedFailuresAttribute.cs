using KJWT.SharedKernel.Results;

namespace KJWT.SharedKernel.AspNetCore;

[AttributeUsage(AttributeTargets.Method)]
public class ExpectedFailuresAttribute : Attribute
{
    public ExpectedFailuresAttribute(params ResultStatus[] resultStatuses)
    {
        ResultStatuses = resultStatuses;
    }

    public IEnumerable<ResultStatus> ResultStatuses { get; }
}
