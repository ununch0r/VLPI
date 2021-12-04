using Core.Entities.Custom.AnswerTemplates;

namespace Core.Entities.Custom.Task
{
    public class WritingTask: BaseTaskModel
    {
        public WritingRequirementsTaskTemplateAnswer StandardAnswer { get; set; }
    }
}
