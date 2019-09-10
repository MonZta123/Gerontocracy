import { RssSource } from './rss-source';

export interface Parliament {
    id: number;
    code: string;
    langtext: string;
    rssSources: RssSource[];
}