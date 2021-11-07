namespace Vlpi.Web.ViewModels.UtilViewModels
{
    public partial class ExecutionModeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public short ExecutionTime { get; set; }
        public byte? WrongRequirementsCount { get; set; }
    }
}
