import { Role } from './role';
import { Post } from './post';
import { Vorfall } from './vorfall';
import { Thread } from './thread';

export interface User {
    id: number;
    registerDate: Date;
    score: number;
    roles: Role[];
    affairs: Vorfall[];
    posts: Post[];
    threads: Thread[];
}