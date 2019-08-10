import { UserOverview } from './user-overview';

export interface SearchResult {
    data: UserOverview[];
    maxResults: number;
}