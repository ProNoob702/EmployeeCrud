import { Component, OnInit, ViewChild } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { MatTable } from "@angular/material/table";
import { ToastrService } from "ngx-toastr";
import { IEmployee, IEmployeeWithoutId } from "src/app/models/IEmployee";
import { EmployeeService } from "src/app/services/employee.service";
import { AddEmployeeComponent } from "../addEmployee/addEmployee.component";
import { AskConfirmComponent } from "../askConfirm/askConfirm.component";
import { EditEmployeeComponent } from "../editEmployee/editEmployee.component";

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
    dialogRef.afterClosed().subscribe((formData: IEmployeeWithoutId | undefined) => {
      if (formData) {
        this.employeeService.Create(formData).subscribe(
          (newEmp) => {
            this.dataSource.push(newEmp);
            this.table?.renderRows();
            this.toastr.success("Employee has been created");
          },
          (err) => {
            this.toastr.error("Employee Creation failed");
          }
        );
      }
    });
  }

  startEdit(index: number, row: IEmployee) {
    const dialogRef = this.dialog.open(EditEmployeeComponent, { width: "500px", data: { ...row } });
    dialogRef.afterClosed().subscribe((formData: IEmployee | undefined) => {
      if (formData) {
        this.employeeService.Update(row.id, formData).subscribe(
          (res) => {
            const dataSrcNewInstance = [...this.dataSource];
            dataSrcNewInstance[index] = formData;
            this.dataSource = dataSrcNewInstance;
            this.table?.renderRows();
            this.toastr.success("Employee has been Updated");
          },
          (err) => {
            this.toastr.error("Employee update failed");
          }
        );
      }
    });
  }

  askConfirmDelete(index: number, row: IEmployee) {
    const dialogRef = this.dialog.open(AskConfirmComponent, { data: { ...row } });
    dialogRef.afterClosed().subscribe((res: boolean | undefined) => {
      if (res) {
        this.employeeService.Delete(row.id).subscribe(
          () => {
            const dataSrcNewInstance = [...this.dataSource];
            dataSrcNewInstance.splice(index, 1);
            this.dataSource = dataSrcNewInstance;
            this.toastr.success("Employee has been deleted");
          },
          () => {
            this.toastr.error("Employee deletion failed");
          }
        );
      }
    });
  }
}
