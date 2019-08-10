import { ThreadOverview } from './thread-overview';

export interface SearchResult {
    data: ThreadOverview[];
    maxResults: number;
}