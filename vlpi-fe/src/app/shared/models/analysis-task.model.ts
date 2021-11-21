import { CreateTask } from "./create-task.model";
import { Requirement } from "./requirement.model";

export interface AnalysisTask extends CreateTask{
    description: string;
    correctRequirements: Requirement[];
    wrongRequirements: Requirement[];
}