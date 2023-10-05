export interface ResponseApi<T> {
    StatusCode: number;
    Success: boolean;
    Result?: T;
    ErrorMessages: string;
}
export interface PagingModel<T> {
    totalCount: number;
    items: T[];
}