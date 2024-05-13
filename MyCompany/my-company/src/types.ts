export interface IDivision {
    id: number,
    name: string,
    description: string | null,
    divisions: IDivision[] | null,
    parentDivisionId: number | null
}
export interface IEmployee {
    id: number,
    firstName: string,
    secondName: string,
    lastName: string,
    birthDay: string,
    gender?: string,
    position?: string,
    driverLicense?: boolean
    divisionid: number
}
export interface Post {
    isSuccess: boolean,
    errorCode: number,
    errorMessage: string,
    data: any
}