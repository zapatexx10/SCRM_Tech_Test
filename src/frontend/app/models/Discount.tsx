export interface Discount {
    type: 'Store' | 'Online' | string; // Used a union type since these seem to be categorical
    originalPrice: number;
    finalPrice: number;
    lowestPriceLast30Days: number;
    priceType: string;
    unitsToBuy: number;
    unitsToPay: number;
    hasPrice: boolean;
}