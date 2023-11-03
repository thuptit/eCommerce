import { DialogMode } from "src/shared/constants";

export interface BaseDataDialog<T> {
    title: string;
    mode: DialogMode;
    model: T
}