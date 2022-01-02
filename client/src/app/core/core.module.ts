import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthInterceptor } from './interceptors/auth.interceptor';
import { AuthGuard } from './guards/auth.guard';



@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ],
  exports: [
    // AuthInterceptor,
    // AuthGuard,
  ]
})
export class CoreModule { }
