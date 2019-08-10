import { ReputationType } from '../../shared/models/reputation-type';
import { QuelleOverview } from './quelle-overview';
import { PolitikerOverview } from './politiker-overview';
import { VoteType } from './vote-type';

export interface VorfallDetail {
    id: number;
    titel: string;
    beschreibung: string;
    erstelltAm: Date;
    reputationType: ReputationType;
    reputation: number;
    quellen: QuelleOverview[];
    politikerOverview: PolitikerOverview;
    userVote: VoteType;
}
