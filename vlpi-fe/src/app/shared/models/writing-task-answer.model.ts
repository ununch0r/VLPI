import { WritingAnswerRequirement } from "./writing-answer-requirement.model";

export interface WritingTaskAnswer{
    taskId: number;
    usedTipsCount: number;
    timeSpent: number;
    systemName: string;
    requirements: WritingAnswerRequirement[];
}