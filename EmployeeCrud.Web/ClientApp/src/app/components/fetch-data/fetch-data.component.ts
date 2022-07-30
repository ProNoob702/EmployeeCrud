import { Component } from '@angular/core';
import { IEmployee } from 'src/app/models/IEmployee';
import { EmployeeService } from 'src/app/services/employee.service';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html',
})
export class FetchDataComponent {
  public employees: IEmployee[] = [];

  constructor(employeeService: EmployeeService) {
    employeeService.getAll().subscribe(
      (res) => {
        this.employees = res;
      },
      (err) => {
        console.error(err);
      }
    );
  }
}
