namespace XProject.Core.Entities
{
    public class User : BaseEntity
    {
        public Security? Security { get; set; }
        public int? SecurityId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
