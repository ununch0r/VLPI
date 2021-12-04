import { CreateTask } from "./create-task.model";
import { RequirementWithContinuation } from "./requirement-with-continuation";

export interface WritingTask extends CreateTask{
    photoUrl: string;
    requirement: RequirementWithContinuation[];
}