import { Component, OnInit } from "@angular/core";
import { AccountService } from "../services/account.service";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.css"]
})
export class LoginComponent implements OnInit {
  username: string = "mmustermann";
  password: string = "1qa<YXCV";

  constructor(public accountService: AccountService) {}

  ngOnInit() {}

  login() {
    this.accountService.login(this.username, this.password);
  }
}
