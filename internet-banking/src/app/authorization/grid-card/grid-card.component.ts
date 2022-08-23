import {Component, Inject } from '@angular/core';
import {MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { DialogData } from './dialog-data';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'grid-card',
  templateUrl: 'grid-card.component.html',
  styleUrls: ['grid-card.component.scss']
})

export class GridCardDialogComponent {
  authorizationForm: FormGroup;

  constructor(
    public dialogRef: MatDialogRef<GridCardDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) {
      this.authorizationForm = new FormGroup({
        pinCode: new FormControl
        ('', [Validators.required, Validators.pattern('^[0-9]+$'), Validators.maxLength(4), Validators.minLength(4)])
      });
    }

  cancel() {
    this.dialogRef.close();
  }

  submit(pin) {
    this.dialogRef.close({pin: parseInt(pin)});
  }
}
