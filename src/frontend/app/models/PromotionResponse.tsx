import type { Promotion } from "./Promotion";

export interface PromotionResponse {
    promotions: Promotion[];
}

export interface PromotionDetailResponse {
    promotion: Promotion;
}