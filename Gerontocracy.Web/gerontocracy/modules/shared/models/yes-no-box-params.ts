import { MessageBoxParams } from './message-box-params';

export interface YesNoBoxParams extends MessageBoxParams {
    yesText: string;
    cancelText: string;
}
