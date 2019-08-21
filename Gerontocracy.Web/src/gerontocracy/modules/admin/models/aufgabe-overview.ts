import { TaskType } from './task-type';

export interface AufgabeOverview {
    id: number;
    taskType: TaskType;
    eingereichtAm: Date;
    uebernommen: boolean;
    einreicher: string;
    erledigt: boolean;
}
