import { TaskType } from './task-type';

export interface AufgabeDetail {
    id: number;
    taskType: TaskType;
    beschreibung: string;
    metaData: string;
    erledigt: boolean;
    eingereichtAm: Date;
    einreicherId: number;
    einreicher: string;
    bearbeiterId: number;
    bearbeiter: string;
}
