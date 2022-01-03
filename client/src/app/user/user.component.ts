import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth/auth.service';
import { UserService } from './user.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {
  crtUserId;

  get users$ () {
    return this.userService.users$;
  }

  constructor (
    private userService: UserService,
    private authService: AuthService
  ) {
    this.crtUserId = authService.crtUserId;
  }

  ngOnInit(): void {
  }

}
