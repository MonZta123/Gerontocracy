export interface ThreadOverview {
    id: number;
    titel: string;
    vorfallId: number;
    vorfallTitel: string;
    politikerId: number;
    politikerName: string;
    createdOn: Date;
    userId: number;
    userName: string;
    numPosts: number;
    generated: boolean;
}
