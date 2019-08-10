export interface PolitikerOverview {
    id: number;
    externalId: number;
    vorname: string;
    nachname: string;
    akadGradPre: string;
    akadGradPost: string;
    wahlkreis: string;
    bundesland: string;
    parteiId?: number;
    reputation: number;
    isRegierung: boolean;
    isNationalrat: boolean;
}
