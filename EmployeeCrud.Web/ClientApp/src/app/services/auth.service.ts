import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { ILoginResult } from "../models/ILoginResult";
import { map, finalize } from "rxjs/operators";
import { BehaviorSubject, Observable, of } from "rxjs";
import { IAppUser } from "../models/IAppUser";
import { LocalStorageService } from "./localStorage.service";
import { Router } from "@angular/router";

@Injectable({
  providedIn: "root",
})
export class AuthService {
  private accountApiBaseUrl: string;
  private _user = new BehaviorSubject<IAppUser | null>(null);
  user$ = this._user.asObservable();

  constructor(
    private http: HttpClient,
    @Inject("BASE_URL") baseUrl: string,
    private localStorageService: LocalStorageService,
    private router: Router
  ) {
    this.accountApiBaseUrl = `${baseUrl}api/account`;
  }

  login(username: string, password: string) {
    return this.http.post<ILoginResult>(`${this.accountApiBaseUrl}/login`, { username, password }).pipe(
      map((x) => {
        this._user.next({
          username: x.username,
          role: x.role,
          originalUserName: x.originalUserName,
        });
        this.setLocalStorage(x);
        // this.startTokenTimer();
        return x;
      })
    );
  }

  logout() {
    this.http
      .post<unknown>(`${this.accountApiBaseUrl}/logout`, {})
      .pipe(
        finalize(() => {
          this.clearLocalStorage();
          this._user.next(null);
          // this.stopTokenTimer();
          this.router.navigate(["login"]);
        })
      )
      .subscribe();
  }

  refreshToken(): Observable<ILoginResult | null> {
    const refreshToken = localStorage.getItem("refresh_token");
    if (!refreshToken) {
      this.clearLocalStorage();
      return of(null);
    }

    return this.http.post<ILoginResult>(`${this.accountApiBaseUrl}/refresh-token`, { refreshToken }).pipe(
      map((x) => {
        this._user.next({
          username: x.username,
          role: x.role,
          originalUserName: x.originalUserName,
        });
        this.setLocalStorage(x);
        // this.startTokenTimer();
        return x;
      })
    );
  }

  setLocalStorage(x: ILoginResult) {
    this.localStorageService.setInfo("access_token", x.accessToken);
    this.localStorageService.setInfo("refresh_token", x.refreshToken);
    this.localStorageService.setInfo("login-event", "login" + Math.random());
  }

  clearLocalStorage() {
    this.localStorageService.clearInfo("access_token");
    this.localStorageService.clearInfo("refresh_token");
    this.localStorageService.setInfo("logout-event", "logout" + Math.random());
  }
}
