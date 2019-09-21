import { ParteiOverview } from './partei-overview';
import { VorfallData } from './vorfall-data';

export interface PolitikerDetail {
    id: number;
    externalId: number;
    name: string;
    wahlkreis: string;
    bundesland: string;
    parteiId?: number;
    partei: ParteiOverview;
    vorfaelle: VorfallData[];
    reputationUp: number;
    reputationDown: number;
    isInactive: boolean;
}
