import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { JwtTokenService } from 'src/app/services/jwt-token.service';
import { RegisterRequestModel } from 'src/app/types/register.model';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  public readonly registerForm: FormGroup;

  constructor(
    private readonly _authService: AuthService,
    private readonly _router: Router
  ) {
    this.registerForm = new FormGroup({
      username: new FormControl<string>("", [Validators.required]),
      email: new FormControl<string>("", [Validators.required, Validators.email]),
      password: new FormControl<string>("", [Validators.required])
    });
  }

  public register(): void {
    const registerRequest: RegisterRequestModel = {
      username: this.registerForm.get("username")?.value ?? "",
      email: this.registerForm.get("email")?.value ?? "",
      password: this.registerForm.get("password")?.value ?? ""
    }

    this._authService.register(registerRequest).subscribe(() => {
      this._router.navigate(["/login"]);
    });
  }
}
