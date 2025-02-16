import { inject, Injectable } from "@angular/core";
import { ChatBotService } from "./chat-bot.service";
import { BehaviorSubject, Observable, tap } from "rxjs";
import { ChatBotModel } from "../types/chat-bot.model";
import { FileUploadModel } from "../types/file.model";


@Injectable({
  providedIn: 'root'
})
export class ChatBotStateService {

    private _chatBotService = inject(ChatBotService);

    private _chatBots: BehaviorSubject<ChatBotModel[]> = new BehaviorSubject<ChatBotModel[]>([]);

    public get chatBots(): Observable<ChatBotModel[]> {
        return this._chatBots.asObservable();
    }

    public set chatBots(chatBots: ChatBotModel[]) {
        this._chatBots.next(chatBots);
    }

    public loadChatBots(): Observable<ChatBotModel[]> {
        return this._chatBotService.getChatBots().pipe(
            tap(chatBots => this._chatBots.next(chatBots))
        );
    }

    public addChatBot(chatBotName: string, files: FileUploadModel[]): Observable<string> {
        return this._chatBotService.createChatBot(chatBotName, files).pipe(
            tap(() => this.loadChatBots().subscribe())
        );
    }

}
