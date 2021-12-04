

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Core.Entities
{
    public partial class Requirement
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string Description { get; set; }
        public bool IsCorrect { get; set; }
        public int? ExplanationId { get; set; }
        public int? ContinuationId { get; set; }
        public int? TypeId { get; set; }

        public virtual Continuation Continuation { get; set; }
        public virtual Explanation Explanation { get; set; }
        public virtual Task Task { get; set; }
        public virtual RequirementType Type { get; set; }
    }
}
