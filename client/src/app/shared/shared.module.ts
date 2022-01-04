import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EnsureOnlyDigitsDirective } from './directives/ensure-only-digits.directive';



@NgModule({
  declarations: [EnsureOnlyDigitsDirective],
  imports: [
    CommonModule
  ],
  exports: [EnsureOnlyDigitsDirective]
})
export class SharedModule { }
