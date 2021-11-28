export interface AnalysisTaskAnswer{
    taskId: number;
    correctRequirements: number[];
    wrongRequirements: number[];
    usedTipsCount: number;
    timeSpent: number;
}