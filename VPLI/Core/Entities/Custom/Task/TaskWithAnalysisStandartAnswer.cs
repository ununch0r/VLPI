using System.Collections.Generic;
using Core.Entities.Custom.AnswerTemplates;

namespace Core.Entities.Custom.Task
{
    public class TaskWithAnalysisStandartAnswer
    {
        public int Id { get; set; }
        public string Objective { get; set; }
        public int Complexity { get; set; }
        public short TypeId { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
        public RequirementsAnalysisTaskTemplateAnswer StandardAnswer { get; set; }

        public virtual ICollection<Requirement> Requirement { get; set; }
        public virtual ICollection<TaskTip> TaskTip { get; set; }
    }
}
