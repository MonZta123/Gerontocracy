import { Role } from './role';

export interface UserDetail {
    id: number;
    userName: string;
    registerDate: Date;
    vorfallCount: number;
    emailConfirmed: boolean;
    accessFailedCount: number;
    banned: boolean;
    lockoutEnd: Date;
    roles: Role[];
}
