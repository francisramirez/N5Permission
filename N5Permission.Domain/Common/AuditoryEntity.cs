

namespace N5Permission.Domain.Common
{
    public abstract class AuditoryEntity
    {
        protected AuditoryEntity()
        {
            this.CreatedDate= DateTime.Now;
            this.IsDeleted= false;
            this.CreatedBy= Environment.UserName;
        }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
