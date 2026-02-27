import { type RouteConfig, index, route } from "@react-router/dev/routes";

// export default [index("routes/home.tsx")] satisfies RouteConfig;

export default [
  index("routes/home.tsx"),
//   route("promotion/:promotionId", "routes/PromotionDetail.tsx"),
route("promotion/:promotionId", "routes/Promotion.$promotionId.tsx"),
] satisfies RouteConfig;
