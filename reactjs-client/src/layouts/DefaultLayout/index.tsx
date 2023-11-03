import { IDefaultLayoutProps } from "./types";

function DefaultLayout(props: IDefaultLayoutProps) {
  return <>{props.children}</>;
}

export default DefaultLayout;
