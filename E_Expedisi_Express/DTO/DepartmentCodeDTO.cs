namespace E_Expedisi_Express.DTO
{
    public class DepartmentCodeDTO
    {
        public string? NewId { get; set; }
        public string? DepartmentName { get; set; }  // Renamed from 'Name'
        public string? DepartmentCode { get; set; }  // Renamed from 'Code'
        public string? Description { get; set; }
        public string? CompCode { get; set; }  // Added field
        public string? DivCode { get; set; }   // Added field
        public bool IsActive { get; set; }
    }
    public class DepartmentCodeAddDto
    {
        public string DepartmentName { get; set; }  // Renamed from 'Name'
        public string DepartmentCode { get; set; }  // Renamed from 'Code'
        public string Description { get; set; }
    }
    public class DepartmentCodeEditDto
    {
        public string? NewId { get; set; }
        public string? DepartmentName { get; set; }  // Renamed from 'Name'
        public string? DepartmentCode { get; set; }  // Renamed from 'Code'
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }

}