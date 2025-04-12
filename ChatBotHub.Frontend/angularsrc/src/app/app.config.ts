import { Injectable } from "@angular/core";


@Injectable({
    providedIn: 'root'
})
export class AppConfig {
    public readonly apiUrl = "http://localhost:5097/api";
}
