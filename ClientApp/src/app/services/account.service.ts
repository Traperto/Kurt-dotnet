import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Account } from "../models/account.model";

@Injectable({
  providedIn: "root"
})
export class AccountService {
  constructor(private http: HttpClient) {}

  login(username, password) {
    //TODO: Logindaten übergeben
    this.http.post("localhost:5001/Login/login", {
      Username: username,
      Password: password
    });
  }

  getUser(): Observable<Account> {
    return this.http.get<Account>(
      "https://localhost:5001/api/Users/GetCurrentUser"
    );
  }
}
