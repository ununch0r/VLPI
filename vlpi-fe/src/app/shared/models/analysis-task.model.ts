import { CreateRequirement } from "./create-requirement.model";
import { CreateTask } from "./create-task.model";

export interface AnalysisTask extends CreateTask{
    description: string;
    correctRequirements: CreateRequirement[];
    wrongRequirements: CreateRequirement[];
}