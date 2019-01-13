import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Account } from "../models/account.model";
import { Token } from "../models/token.model";

@Injectable({
  providedIn: "root"
})
export class AccountService {
  constructor(private http: HttpClient) {}

  login(username, password) {
    this.http
      .post<Token>("/api/token/", {
        Username: username,
        Password: password
      })
      .subscribe(data => {
        console.log(data);
        localStorage.setItem("token", data.token);
      });
  }

  getUser(): Observable<Account> {
    const headers = new HttpHeaders({
      "Content-Type": "application/json",
      Authorization: "Bearer " + localStorage.getItem("token")
    });

    console.log(headers);
    return this.http.get<Account>(
      "/api/Users/GetCurrentUser",
      {
        headers: headers
      }
    );
  }
}
