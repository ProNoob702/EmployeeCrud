import { Component, Inject, OnInit } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { IEmployeeWithoutId } from "src/app/models/IEmployee";
import { EmployeeService } from "src/app/services/employee.service";

@Component({
  selector: "app-addEmployee",
  templateUrl: "./addEmployee.component.html",
  styleUrls: ["./addEmployee.component.scss"],
})
export class AddEmployeeComponent implements OnInit {
  form: FormGroup;
  public submitted: boolean = false;

  constructor(
    public dialogRef: MatDialogRef<AddEmployeeComponent>,
    @Inject(MAT_DIALOG_DATA) public data: IEmployeeWithoutId,
    public employeeService: EmployeeService
  ) {
    this.form = new FormGroup({
      email: new FormControl("", [Validators.required, Validators.email]),
      firstName: new FormControl("", [Validators.required]),
      lastName: new FormControl("", [Validators.required]),
    });
  }

  get f() {
    return this.form.controls;
  }

  ngOnInit() {}

  onClickSubmit(formData: IEmployeeWithoutId) {
    this.submitted = true;

    // stop here if form is invalid
    if (this.form.invalid) {
      return;
    }

    this.dialogRef.close(formData);
  }

  onCancelClick(): void {
    this.dialogRef.close();
  }
}
