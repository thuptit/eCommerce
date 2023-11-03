import { BaseDataDialog } from "./base.model";
import { GridParam } from "./filter-request.model";

export interface UserModel{
    id: number;
    userName: string;
    phoneNumber: string;
    email: string;
    address: string;
    avatarUrl: string;
    createdBy: string;
    creationTime: string;
}

export interface UserGridParam extends GridParam{
}

export interface UserDataDialog extends BaseDataDialog<UserModel>{
}