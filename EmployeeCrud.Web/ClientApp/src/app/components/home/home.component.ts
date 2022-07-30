import { Component, OnInit, ViewChild } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { MatTable } from "@angular/material/table";
import { ToastrService } from "ngx-toastr";
import { IEmployee } from "src/app/models/IEmployee";
import { EmployeeService } from "src/app/services/employee.service";
import { AddEmployeeComponent } from "../addEmployee/addEmployee.component";

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.scss"],
})
export class HomeComponent implements OnInit {
  displayedColumns = ["id", "email", "firstName", "lastName", "actions"];
  dataSource: IEmployee[] = [];
  constructor(public employeeService: EmployeeService, public dialog: MatDialog, private toastr: ToastrService) {}

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

  addNew() {
    const dialogRef = this.dialog.open(AddEmployeeComponent, { width: "500px" });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.dataSource.push(result);
        this.table?.renderRows();
        this.toastr.success("Employee has been created");
      }
    });
  }
}
