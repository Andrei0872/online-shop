import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormControlDirective, FormGroup, NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthField, LoginBody, RegisterBody } from './auth.model';
import { AuthService } from './auth.service';

const LOGIN_FIELDS: AuthField[] = [
  {
    propName: 'email',
    humanReadableName: 'Email',
    defaultValue: '',
  },
  {
    propName: 'password',
    humanReadableName: 'Password',
    defaultValue: '',
  }
];

const REGISTER_FIELDS: AuthField[] = [
  {
    propName: 'firstName',
    humanReadableName: 'First name',
    defaultValue: '',
  },
  {
    propName: 'lastName',
    humanReadableName: 'Last name',
    defaultValue: '',
  }
].concat(LOGIN_FIELDS);

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss']
})
export class AuthComponent implements OnInit {
  private isRegisterMode = true;

  form: FormGroup;

  LOGIN_FIELDS = LOGIN_FIELDS;
  REGISTER_FIELDS = REGISTER_FIELDS;

  get authFields (): AuthField[] {
    return this.isRegisterMode ? REGISTER_FIELDS : LOGIN_FIELDS;
  }

  get headerContent () {
    return this.isRegisterMode ? 'Register' : 'Log in';
  }

  get changeModeMessage () {
    return this.isRegisterMode
      ? 'Already have an account?'
      : `Don't have an account yet?`;
  }

  constructor (
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.form = this.fb.group(this.getFieldsForFormGroup());
  }

  ngOnInit(): void {
  }

  private getFieldsForFormGroup(): { [k: string]: AbstractControl } {
    return this.authFields.reduce(
      (acc, crtField) => ({
        ...acc,
        [crtField.propName]: this.fb.control(crtField.defaultValue),
      }),
      {}
    );
  }

  private refreshFormGroup () {
    this.form.reset();
    this.form = this.fb.group(this.getFieldsForFormGroup());
  }

  onSubmit (formValues: any) {
    let body;
    let fnToCall;
    if (this.isRegisterMode) {
      body = formValues as RegisterBody;
      fnToCall = this.authService.attemptRegister.bind(this.authService, body);
    } else {
      body = formValues as LoginBody;
      fnToCall = this.authService.attemptLogin.bind(this.authService, body);
    }

    fnToCall().subscribe({
      next: () => this.router.navigateByUrl('/products'),
      error: (errObj) => alert(errObj.error.error)
    });
  }

  changeMode () {
    this.isRegisterMode = !this.isRegisterMode;

    this.refreshFormGroup();
  }
}
