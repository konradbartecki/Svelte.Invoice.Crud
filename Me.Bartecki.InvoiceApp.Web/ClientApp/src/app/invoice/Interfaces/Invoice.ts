import { PaymentType } from "./PaymentType";
export interface Invoice {
    id: number;
    year: number;
    month: number;
    clientName: string;
    sellDate: Date;
    issueDate: Date;
    paytime: number;
    paymentType: PaymentType;
    isPaid: boolean;
    netTotal: number;
    grossTotal: number;
}
