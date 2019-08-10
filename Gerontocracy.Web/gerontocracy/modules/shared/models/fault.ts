export interface Fault {
    message: string;
    name: string;
    stackTrace: string;
    innerfault: Fault;
}