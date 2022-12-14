import { Inject, Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { IEmployee, IEmployeeWithoutId } from "../models/IEmployee";

@Injectable({
  providedIn: "root",
})
export class EmployeeService {
  private employeeApiBaseUrl: string;

  constructor(private http: HttpClient, @Inject("BASE_URL") baseUrl: string) {
    this.employeeApiBaseUrl = `${baseUrl}api/employees`;
  }

  getAll() {
    return this.http.get<IEmployee[]>(this.employeeApiBaseUrl);
  }

  get(id: number) {
    const url = `${this.employeeApiBaseUrl}/${id}`;
    return this.http.get<IEmployee>(url);
  }

  Create(newEmp: IEmployeeWithoutId) {
    return this.http.post<IEmployee>(this.employeeApiBaseUrl, newEmp);
  }

  Update(id: number, updatedEmp: IEmployee) {
    const url = `${this.employeeApiBaseUrl}/${id}`;
    return this.http.put<IEmployee>(url, updatedEmp);
  }

  Delete(id: number) {
    const url = `${this.employeeApiBaseUrl}/${id}`;
    return this.http.delete(url);
  }
}
