import { Component, NgZone, OnInit } from "@angular/core";
import { Drink } from "../models/drink.model";
import { DrinkContainment } from "../models/drinkContainment.model";
import { DrinkService } from "../services/drink-service.service";

@Component({
  selector: "kurt-refill",
  templateUrl: "./refill.component.html",
  styleUrls: ["./refill.component.scss"]
})
export class RefillComponent implements OnInit {
  constructor(public drinkService: DrinkService, private zone: NgZone) {}

  drinks: Drink[];
  currentDrink: Drink;
  refilledDrinks: DrinkContainment[] = [];

  ngOnInit() {
    this.drinkService.getUser().subscribe(data => {
      this.zone.run(() => {
        this.drinks = data;
        console.log(this.drinks);
      });
    });
  }

  add() {
    console.log(this.currentDrink);

    //let drinkContainment: DrinkContainment = {
    //drink: this.currentDrink,
    //amount: 0
    //};

    //this.refilledDrinks.push(drinkContainment);
    console.log(this.refilledDrinks);
  }
}
