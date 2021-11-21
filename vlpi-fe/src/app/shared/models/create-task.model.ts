import { TaskTip } from "./task-tip.model";

export interface CreateTask{
    objective: string;
    complexity: number;
    typeId : string;
    taskTip: TaskTip[];
}