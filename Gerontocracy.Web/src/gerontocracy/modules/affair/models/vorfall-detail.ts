import { ReputationType } from '../../shared/models/reputation-type';
import { QuelleOverview } from './quelle-overview';
import { PolitikerOverview } from './politiker-overview';
import { VoteType } from './vote-type';
import { User } from '../../../models/user';

export interface VorfallDetail {
    id: number;
    titel: string;
    beschreibung: string;
    erstelltAm: Date;
    erstelltVon: User;
    reputationType: ReputationType;
    reputation: number;
    quellen: QuelleOverview[];
    politiker: PolitikerOverview;
    userVote: VoteType;
}
