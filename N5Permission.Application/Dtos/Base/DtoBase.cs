namespace N5Permission.Application.Dtos.Base
{
    public abstract record DtoBase<Type>
    {
        public Type Id { get; set; }
        public string? ChangedBy { get; set; }
        public DateTime ChangedDate { get; set; }
    }
}
