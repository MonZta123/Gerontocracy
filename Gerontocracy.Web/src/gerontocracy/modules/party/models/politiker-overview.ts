export interface PolitikerOverview {
    id: number;
    externalId: number;
    name: string;
    wahlkreis: string;
    bundesland: string;
    parteiId?: number;
    parteiKurzzeichen: string;
    reputation: number;
}
