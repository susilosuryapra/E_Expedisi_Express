namespace E_Expedisi_Express.DTO
{
    public class DepartmentCodeDTO
    {
        public int Id { get; set; }
        public Guid? NewId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
