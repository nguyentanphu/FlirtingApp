import { AbstractControl, ValidatorFn } from '@angular/forms';

export function maxDate(date: Date): ValidatorFn {
  return (control: AbstractControl) => {
    if (control.value > date) {
      return { maxdate: true };
    }

    return null;
  }
}