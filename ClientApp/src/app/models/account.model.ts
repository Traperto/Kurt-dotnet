import { Proceed } from "./proceed.model";

export interface Account {
  drinks: [];
  firstName: string;
  id: number;
  lastName: string;
  proceeds: Proceed[];
  userName: string;
}
