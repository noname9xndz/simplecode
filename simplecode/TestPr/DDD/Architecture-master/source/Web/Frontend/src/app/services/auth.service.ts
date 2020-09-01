import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";

@Injectable({ providedIn: "root" })
export class AppAuthService {
    constructor(
        private readonly http: HttpClient,
        private readonly router: Router) { }

    authenticated() {
        return this.token() !== null;
    }

    login(model: any): void {
        this.http
            .post("auths", model)
            .subscribe((result: any) => {
                if (!result || !result.token) { return; }
                localStorage.setItem("token", result.token);
                this.router.navigate(["/main/home"]);
            });
    }

    signin() {
        this.router.navigate(["/login"]);
    }

    signout() {
        localStorage.clear();
        sessionStorage.clear();
        this.signin();
    }

    token() {
        return localStorage.getItem("token");
    }
}
