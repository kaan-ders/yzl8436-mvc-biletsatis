namespace BiletSatis.Models
{
    public abstract class ModelBase
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}