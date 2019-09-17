import { RssSource } from './rss-source';

export interface Parliament {
    id: number;
    code: string;
    langtext: string;
    sources: RssSource[];
}