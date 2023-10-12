export interface ResponseApi<T> {
    StatusCode: number;
    Success: boolean;
    Result?: T;
    ErrorMessages: string;
    isLoading: boolean;
}
export interface PagingModel<T> {
    totalCount: number;
    items: T[];
}