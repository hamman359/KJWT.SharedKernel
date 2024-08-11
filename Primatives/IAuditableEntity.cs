namespace KJWT.SharedKernel.Primatives;

public interface IAuditableEntity
{
    DateTime CreatedOnUtc { get; set; }

    DateTime? ModifiedOnUtc { get; set; }
}