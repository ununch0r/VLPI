import { WritingRequirementResult } from "./writing-requirement-result.model";

export interface WritingTaskResult{
    score: number;
    timeSpent: number;
    systemName: string;
    requirements: WritingRequirementResult[];
}