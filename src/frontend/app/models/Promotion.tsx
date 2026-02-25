import type {PromotionTexts} from "./PromotionTexts.tsx";
import type {Discount} from "./Discount.tsx";

export interface Promotion {
    promotionId: string;
    texts: PromotionTexts;
    images: string[];
    discounts: Discount[];
}