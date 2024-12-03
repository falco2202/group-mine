namespace Domain.Enitity
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; } = String.Empty;
        public DateTime? LastModified { get; set; }
        public string LastModifiedBy { get; set; } = String.Empty;
    }
}
