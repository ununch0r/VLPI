import { RequirementWithExplanation } from "./requirement-with-explanation.model";
import { CreateTask } from "./create-task.model";

export interface AnalysisTask extends CreateTask{
    description: string;
    correctRequirements: RequirementWithExplanation[];
    wrongRequirements: RequirementWithExplanation[];
}