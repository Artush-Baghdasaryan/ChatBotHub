import { Injectable } from "@angular/core";
import { UserModel } from "../types/user.model";

@Injectable({
    providedIn: "root"
})
export class JwtTokenService {

    private readonly tokenKey = "jwt-token";
    private readonly usernameKey = "username";
    private readonly emailKey = "email";

    public saveToken(token: string): void {
        localStorage.setItem(this.tokenKey, token);
    }

    public getToken(): string | null {
        return localStorage.getItem(this.tokenKey);
    }

    public removeToken(): void {
        localStorage.removeItem(this.tokenKey);
    }

    public saveUserData(user: UserModel): void {
        localStorage.setItem(this.usernameKey, user.username);
        localStorage.setItem(this.emailKey, user.email);
    }

    public getUserData(): UserModel {
        return {
            username: localStorage.getItem(this.usernameKey) ?? "",
            email: localStorage.getItem(this.emailKey) ?? ""
        };
    }

}