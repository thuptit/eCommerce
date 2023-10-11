import { GridParam } from "./filter-request.model";

export interface CategoryModel {
    id: number;
    name: string;
    description: string;
}
export interface CategoryGridParam extends GridParam {
}

export interface CategoryDataDialog {
    title: string;
    model: CategoryModel;
}