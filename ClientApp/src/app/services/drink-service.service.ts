import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Drink } from "../models/drink.model";

@Injectable({
  providedIn: "root"
})
export class DrinkService {
  constructor(private http: HttpClient) {}

  getUser(): Observable<Drink[]> {
    return this.http.get<Drink[]>("/api/Drink/getAll");
  }
}
