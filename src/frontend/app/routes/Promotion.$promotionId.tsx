import PromotionDetail from "../promotion-detail/PromotionDetail";
// import type { Route } from "./+types/Promotion.$promotionId";

export function meta({ params }: any) {
  return [
    { title: `Promotion ${params.promotionId}` },
    { name: "description", content: "Promotion details" },
  ];
}

export default function PromotionDetailRoute() {
  return <PromotionDetail />;
}