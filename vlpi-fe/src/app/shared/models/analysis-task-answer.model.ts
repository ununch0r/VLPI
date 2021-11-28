import { CreateWrongRequirement } from "./create-wrong-requirement.model";

export interface AnalysisTaskAnswer{
    expectedWrongRequirementsCount: number;
    taskId: number;
    correctRequirements: number[];
    wrongRequirements: CreateWrongRequirement[];
    usedTipsCount: number;
    timeSpent: number;
}