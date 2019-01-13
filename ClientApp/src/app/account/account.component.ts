import { Component, NgZone, OnInit } from "@angular/core";
import { Account } from "../models/account.model";
import { AccountService } from "../services/account.service";

@Component({
  selector: "kurt-account",
  templateUrl: "./account.component.html",
  styleUrls: ["./account.component.scss"]
})
export class AccountComponent implements OnInit {
  constructor(public accountService: AccountService, private zone: NgZone) {}

  account: Account;

  ngOnInit() {
    this.accountService.getUser().subscribe(data => {
      this.zone.run(() => {
        this.account = data;
        console.log(this.account);
      });
    });
  }
}
