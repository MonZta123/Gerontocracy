import { ReputationType } from '../../shared/models/reputation-type';

export interface NewsData {
    newsId: number;
    beschreibung: string;
    politikerId?: number;
    reputationType: ReputationType;
}
