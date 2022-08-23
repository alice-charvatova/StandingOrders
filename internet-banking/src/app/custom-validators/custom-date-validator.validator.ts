import { AbstractControl } from "@angular/forms";

export function validateDateIsLaterThanToday(control: AbstractControl) {
  let currentDate = new Date();
  let newDate = new Date(control.value);
  if (newDate <= currentDate) {
    return { 'validateDate': true };
  }
  return null;
}

