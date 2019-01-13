import { TestBed } from "@angular/core/testing";
import { DrinkServiceService } from "./drink-service.service";

describe("DrinkServiceService", () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it("should be created", () => {
    const service: DrinkServiceService = TestBed.get(DrinkServiceService);
    expect(service).toBeTruthy();
  });
});
