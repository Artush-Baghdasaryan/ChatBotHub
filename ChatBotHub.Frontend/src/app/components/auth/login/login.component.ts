import { Component, inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { JwtTokenService } from 'src/app/services/jwt-token.service';
import { LoginRequestModel } from 'src/app/types/login.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  
  public readonly loginForm: FormGroup;

  constructor(
    private readonly _authService: AuthService,
    private readonly _jwtTokenService: JwtTokenService,
    private readonly _router: Router
  ) {
    this.loginForm = new FormGroup({
      email: new FormControl<string>("", [Validators.required, Validators.email]),
      password: new FormControl<string>("", [Validators.required])
    });
  }

  public login(): void {
    const loginRequest: LoginRequestModel = {
      email: this.loginForm.get("email")?.value ?? "",
      password: this.loginForm.get("password")?.value ?? ""
    }

    this._authService.login(loginRequest).subscribe(result => {
      this._jwtTokenService.saveToken(result.token);
      this._authService.me().subscribe();
      this._router.navigate(["/"]);
    });
  }
}
