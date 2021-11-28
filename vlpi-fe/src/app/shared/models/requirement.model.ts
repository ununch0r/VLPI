export interface Requirement{
    id: number;
    description: string;
    isCorrect: boolean;
    explanationId: number | null;
}