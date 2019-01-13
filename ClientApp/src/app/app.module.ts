import { HttpClientModule } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";
import { RouterModule } from "@angular/router";
import { AccountComponent } from "./account/account.component";
import { AppComponent } from "./app.component";
import { HomeComponent } from "./home/home.component";
import { LoginComponent } from "./login/login.component";
import { NavMenuComponent } from "./nav-menu/nav-menu.component";
import { RefillComponent } from "./refill/refill.component";

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    LoginComponent,
    AccountComponent,
    RefillComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: "ng-cli-universal" }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot(
      [
        { path: "", component: HomeComponent, pathMatch: "full" },
        { path: "login", component: LoginComponent, pathMatch: "full" },
        { path: "account", component: AccountComponent },
        { path: "refill", component: RefillComponent }
      ],
      { useHash: true }
    )
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}
