import type { Route } from "./+types/home";
import PromotionList from "../promotion-list/PromotionList";

export function meta({}: Route.MetaArgs) {
  return [
    { title: "PromotionEngine" },
    { name: "description", content: "Promotion Engine" },
  ];
}

export default function Home() {
  return <PromotionList />;
}
