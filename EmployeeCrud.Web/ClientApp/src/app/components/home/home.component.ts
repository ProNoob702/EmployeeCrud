import { Component, OnInit, ViewChild } from "@angular/core";
import { MatTable } from "@angular/material/table";
import { IEmployee } from "src/app/models/IEmployee";
import { EmployeeService } from "src/app/services/employee.service";

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.scss"],
})
export class HomeComponent implements OnInit {
  displayedColumns = ["id", "email", "firstName", "lastName", "actions"];
  dataSource: IEmployee[] = [];
  constructor(public employeeService: EmployeeService) {}

  @ViewChild(MatTable, { static: true }) table: MatTable<IEmployee> | undefined;

  ngOnInit() {
    this.employeeService.getAll().subscribe(
      (res) => {
        this.dataSource = res;
        this.table?.renderRows();
      },
      (err) => {
        console.error(err);
      }
    );
  }
}
