import { VorfallOverview } from './vorfall-overview';

export interface SearchResult {
    data: VorfallOverview[];
    maxResults: number;
}