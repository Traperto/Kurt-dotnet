import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable({
  providedIn: "root"
})
export class AccountService {
  constructor(private http: HttpClient) {}

  login(username, password) {
    //TODO: Logindaten übergeben
    this.http.post("localhost:5001/Login/login", {
      Username: "mmustermann",
      Password: "1qa<YXCV"
    });
  }

  getUser() {
    this.http
      .get("https://localhost:5001/api/Users/getUser/1")
      .subscribe(data => {
        console.log(data);
      });
  }
}
