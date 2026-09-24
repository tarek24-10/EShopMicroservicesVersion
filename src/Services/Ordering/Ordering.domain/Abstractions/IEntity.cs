namespace Ordering.domain.Abstractions
{
    public interface IEntity
    {
        DateTime? CreatedAt { get; set; }
        string? CreatedBy { get; set; }
        DateTime? LastModifiedAt { get; set; }
        string? LastModifiedBy { get; set; }
    }

    public interface IEntity<T> : IEntity
    {
        T Id { get; set; }
    }
}
