export interface AuthenticateModel {
    userName: string;
    userId: number;
    roles: string[];
    email: string;
    accessToken: string;
}