import { RequirementWithExplanation } from "./requirement-with-explanation.model";

export interface AnalysisTaskResult{
    score: number;
    timeSpent: number;
    correctRequirements: string[]
    wrongRequirements: RequirementWithExplanation[]
}