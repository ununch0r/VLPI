import { CreateTask } from "./create-task.model";
import { RequirementWithContinuation } from "./requirement-with-continuation";

export interface WritingTask extends CreateTask{
    systemNames: string[];
    photoUrl: string;
    requirements: RequirementWithContinuation[];
}