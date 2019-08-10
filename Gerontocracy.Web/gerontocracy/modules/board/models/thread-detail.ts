import { Post } from './post';

export interface ThreadDetail {
    id: number;
    titel: string;
    vorfallId: number;
    vorfallTitel: string;
    politikerId: number;
    politikerName: string;
    initialPost: Post;
}
