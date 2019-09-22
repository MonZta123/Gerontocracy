import { Fault } from './fault';

export interface PostResult<T> {
    success: boolean;
    data: T;
    errors: Fault[];
}