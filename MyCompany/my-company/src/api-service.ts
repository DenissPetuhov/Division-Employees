import axios from 'axios';

const URL = 'https://localhost:7197/api/';
const divisionController = 'Division';
const employeeController = "Employee";

export const divisionControllerApi = axios.create({
    baseURL: URL + divisionController,
})

export const employeeControllerApi = axios.create({
    baseURL: URL + employeeController,
})

