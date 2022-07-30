import { Component, Inject, OnInit } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { IEmployee, IEmployeeWithoutId } from "src/app/models/IEmployee";
import { EmployeeService } from "src/app/services/employee.service";

@Component({
  selector: "app-editEmployee",
  templateUrl: "./editEmployee.component.html",
  styleUrls: ["./editEmployee.component.scss"],
})
export class EditEmployeeComponent implements OnInit {
  form: FormGroup;
  public submitted: boolean = false;

  constructor(
    public dialogRef: MatDialogRef<EditEmployeeComponent>,
    @Inject(MAT_DIALOG_DATA) public data: IEmployee,
    public employeeService: EmployeeService
  ) {
    this.form = new FormGroup({
      email: new FormControl(data.email, [Validators.required, Validators.email]),
      firstName: new FormControl(data.firstName, [Validators.required]),
      lastName: new FormControl(data.lastName, [Validators.required]),
    });
  }

  get f() {
    return this.form.controls;
  }

  ngOnInit() {
    console.log("data", this.data);
  }

  onClickSubmit(formData: IEmployeeWithoutId) {
    this.submitted = true;

    // stop here if form is invalid
    if (this.form.invalid) {
      return;
    }

    this.dialogRef.close({ ...formData, id: this.data.id });
  }

  onCancelClick(): void {
    this.dialogRef.close();
  }
}
