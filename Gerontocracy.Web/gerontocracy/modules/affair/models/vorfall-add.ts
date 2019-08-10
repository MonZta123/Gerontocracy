import { ReputationType } from '../../shared/models/reputation-type';
import { QuelleAdd } from './quelle-add';

export interface VorfallAdd {
    titel: string;
    beschreibung: string;
    reputationType?: ReputationType;
    quellen: QuelleAdd[];
    politikerId?: number;
}
