import { Error } from './error';

export interface Result {
    errors: Error[];
    succeeded: boolean;
}