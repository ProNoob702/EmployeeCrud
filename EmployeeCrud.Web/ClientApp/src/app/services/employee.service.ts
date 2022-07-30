import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { IEmployee } from '../models/IEmployee';

const headers: HttpHeaders['headers'] = {
  'Content-Type': 'application/json',
};

@Injectable({
  providedIn: 'root',
})
export class EmployeeService {
  private employeeApiBaseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.employeeApiBaseUrl = `${baseUrl}api/employees`;
  }

  getAll() {
    return this.http.get<IEmployee[]>(this.employeeApiBaseUrl, {
      headers: headers,
    });
  }

  get(id: number) {
    const url = `${this.employeeApiBaseUrl}/${id}`;
    return this.http.get<IEmployee>(url, {
      headers: headers,
    });
  }

  Create(newEmp: IEmployee) {
    return this.http.post<IEmployee>(this.employeeApiBaseUrl, newEmp, {
      headers: headers,
    });
  }

  Update(id: number, updatedEmp: IEmployee) {
    const url = `${this.employeeApiBaseUrl}/${id}`;
    return this.http.put<IEmployee>(url, updatedEmp, {
      headers: headers,
    });
  }

  Delete(id: number) {
    const url = `${this.employeeApiBaseUrl}/${id}`;
    return this.http.delete(url, {
      headers: headers,
    });
  }
}
