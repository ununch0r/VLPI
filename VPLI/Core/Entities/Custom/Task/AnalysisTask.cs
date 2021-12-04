using Core.Entities.Custom.AnswerTemplates;

namespace Core.Entities.Custom.Task
{
    public class AnalysisTask : BaseTaskModel
    {
        public RequirementsAnalysisTaskTemplateAnswer StandardAnswer { get; set; }
    }
}
