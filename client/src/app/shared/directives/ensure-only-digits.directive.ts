import { Directive, HostListener } from '@angular/core';

import { AbstractControl, ControlValueAccessor, FormControl, NgControl } from '@angular/forms'

@Directive({
  selector: '[ensureOnlyDigits]'
})
export class EnsureOnlyDigitsDirective {

  constructor (private ngControl: NgControl) {
    // Doing the logic in the next tick so that the `control` property
    // is available.
    setTimeout(() => {
      ngControl.control?.valueChanges
        .subscribe((v: string) => {
          const expectedValue = v.replace(/[^\d]/g, '');
          if (expectedValue === v) {
            return;
          }

          ngControl.control?.setValue(expectedValue);
        });
    }, 0);
  }
}
