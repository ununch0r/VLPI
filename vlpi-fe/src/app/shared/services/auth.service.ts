import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Token } from '../models/token.model';
import { User } from '../models/user.model';

export const ACCESS_TOKEN_KEY = 'vlpi_access_token'

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(
    private http : HttpClient,
    private jwtHelper : JwtHelperService,
    private router : Router
  ) {}

  login(email:string, password:string) : Observable<Token> {
    return this.http.post<Token>('auth/login',{
      email, password
    }).pipe(
      tap(token => {
        localStorage.setItem(ACCESS_TOKEN_KEY, token.access_token);
      })
    );
  }

  register(user : User) : Observable<Token>{
    return this.http.post<Token>('auth/register', user).pipe(
      tap(token => {
        localStorage.setItem(ACCESS_TOKEN_KEY, token.access_token);
      })
    );
  }

  isAuthenticated() : boolean{
    var token = localStorage.getItem(ACCESS_TOKEN_KEY);
    return token && !this.jwtHelper.isTokenExpired(token);
  }

  logout() : void {
    localStorage.removeItem(ACCESS_TOKEN_KEY);
    this.router.navigate(['auth']);
  }
}
