import Home from "../pages/Home";
import { DefaultLayout } from "../layouts";

const publicRouters = [
  {
    path: "/",
    component: Home,
    layout: DefaultLayout,
  },
];

const privateRouters: never[] = [];

export { publicRouters, privateRouters };
