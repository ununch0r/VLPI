import { Explanation } from "./explanation.model";
import { Requirement } from "./requirement.model";
import { TaskTip } from "./task-tip.model";
import { TaskType } from "./task-type.model";

export interface Task{
    id: number;
    objective: string;
    order: number;
    description : string;
    photoUrl: string;
    type: TaskType;
    requirement: Requirement[];
    taskTip: TaskTip[];
    explanation: Explanation[];
}