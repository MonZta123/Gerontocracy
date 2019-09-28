import { Role } from './role';
import { Post } from './post';
import { Vorfall } from './vorfall';

export interface User {
    id: number;
    registerDate: Date;
    score: number;
    roles: Role[];
    affairs: Vorfall[];
    posts: Post[];
}