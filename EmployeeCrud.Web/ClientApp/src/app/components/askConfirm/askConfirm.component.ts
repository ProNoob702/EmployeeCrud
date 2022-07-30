import { Component, Inject, OnInit } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { IEmployee, IEmployeeWithoutId } from "src/app/models/IEmployee";

@Component({
  selector: "app-askConfirm",
  templateUrl: "./askConfirm.component.html",
  styleUrls: ["./askConfirm.component.css"],
})
export class AskConfirmComponent implements OnInit {
  constructor(public dialogRef: MatDialogRef<AskConfirmComponent>, @Inject(MAT_DIALOG_DATA) public data: IEmployee) {}

  ngOnInit() {}

  onClickOk() {
    this.dialogRef.close(true);
  }

  onCancelClick(): void {
    this.dialogRef.close();
  }
}
