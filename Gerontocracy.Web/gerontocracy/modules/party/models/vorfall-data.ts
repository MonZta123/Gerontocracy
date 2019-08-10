import { User } from './user';

export interface VorfallData {
    id: number;
    titel: string;
    erstelltAm: Date;
    erstelltVon: User;
}