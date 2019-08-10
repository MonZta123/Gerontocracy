export interface UserDetail {
    id: number;
    userName: string;
    registerDate: Date;
    vorfallCount: number;
    emailConfirmed: boolean;
    accessFailedCount: number;
    lockoutEnd: Date;
    roles: string[];
}
