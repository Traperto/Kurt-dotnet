import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Account } from "../models/account.model";

@Injectable({
  providedIn: "root"
})
export class AccountService {
  constructor(private http: HttpClient) {}

  login(username, password) {
    this.http
      .post("https://localhost:5001/api/token/", {
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
    return this.http.get("https://localhost:5001/api/Users/GetCurrentUser", {
      headers: headers
    });
  }
}
