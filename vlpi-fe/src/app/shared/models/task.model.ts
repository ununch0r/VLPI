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
    Requirement: Requirement[];
    TaskTip: TaskTip[];
}