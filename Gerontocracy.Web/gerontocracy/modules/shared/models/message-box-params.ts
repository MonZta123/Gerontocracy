import { IconType } from './icon-type';

export interface MessageBoxParams {
    title?: string;
    icon?: IconType;
    message: string;
}
