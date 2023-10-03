export interface ResponseApi<T> {
    StatusCode: number;
    Success: boolean;
    Result?: T;
    ErrorMessages: string;
}