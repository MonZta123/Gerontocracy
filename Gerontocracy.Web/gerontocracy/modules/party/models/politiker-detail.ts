import { ParteiOverview } from './partei-overview';
import { VorfallData } from './vorfall-data';

export interface PolitikerDetail {
    id: number;
    externalId: number;
    vorname: string;
    nachname: string;
    akadGradPre: string;
    akadGradPost: string;
    wahlkreis: string;
    bundesland: string;
    isRegierung: boolean;
    isNationalrat: boolean;
    parteiId?: number;
    partei: ParteiOverview;
    vorfaelle: VorfallData[];
    reputationUp: number;
    reputationDown: number;
}
