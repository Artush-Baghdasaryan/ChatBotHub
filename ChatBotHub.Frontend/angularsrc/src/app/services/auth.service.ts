import { inject, Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { LoginRequestModel, LoginResponseModel } from "../types/login.model";
import { Observable, tap } from "rxjs";
import { AppConfig } from "../app.config";
import { RegisterRequestModel } from "../types/register.model";
import { UserModel } from "../types/user.model";
import { JwtTokenService } from "./jwt-token.service";

@Injectable({
    providedIn: "root"
})
export class AuthService {

    private readonly _http = inject(HttpClient);
    private readonly _appConfig = inject(AppConfig);
    private readonly _jwtTokenService = inject(JwtTokenService);

    private readonly apiUrl = this._appConfig.apiUrl;
    private readonly mainRoute = "/Auth";

    public login(loginRequest: LoginRequestModel): Observable<LoginResponseModel> {
        return this._http.post<LoginResponseModel>(`${this.apiUrl}${this.mainRoute}/login`, loginRequest);
    }

    public register(registerRequest: RegisterRequestModel): Observable<void> {
        return this._http.post<void>(`${this.apiUrl}${this.mainRoute}/register`, registerRequest);
    }

    public me(): Observable<UserModel> {
        return this._http.get<UserModel>(`${this.apiUrl}/User/me`).pipe(
            tap(user => {
                this._jwtTokenService.saveUserData(user);
            })
        );
    }

}