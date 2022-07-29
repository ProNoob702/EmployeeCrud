import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html',
})
export class FetchDataComponent {
  public employees: IEmployee[] = [];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<IEmployee[]>(baseUrl + 'api/employees').subscribe(
      (result) => {
        this.employees = result;
      },
      (error) => console.error(error)
    );
  }
}

interface IEmployee {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
}
