import { Game } from "./game.model";

export class User {
    userName?:string;
    games:Array<Game> = [];
}
