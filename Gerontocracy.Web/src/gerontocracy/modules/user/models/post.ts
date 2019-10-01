export interface Post {
    id: number;
    content: string;
    createdOn: Date;
    likes: number;
    dislikes: number;
    threadId: number;
}
