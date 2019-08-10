import { LikeType } from './like-type';

export interface Post {
    id: number;
    content: string;
    createdOn: Date;
    userId: number;
    userName: string;
    userLike: LikeType;
    likes: number;
    dislikes: number;
    children: Post[];
}
