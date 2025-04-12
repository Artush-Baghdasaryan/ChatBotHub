import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { AppConfig } from '../app.config';
import { ChatBotModel } from '../types/chat-bot.model';
import { Observable } from 'rxjs';
import { FileUploadModel } from '../types/file.model';

@Injectable({
  providedIn: 'root'
})
export class ChatBotService {

  private readonly _http = inject(HttpClient);
  private readonly _appConfig = inject(AppConfig);

  private readonly apiUrl = this._appConfig.apiUrl;
  private readonly mainRoute = "/ChatBot";

  public getChatBot(id: string): Observable<ChatBotModel> {
    return this._http.get<ChatBotModel>(`${this.apiUrl}${this.mainRoute}/${id}`);
  }

  public getChatBots(): Observable<ChatBotModel[]> {
    return this._http.get<ChatBotModel[]>(`${this.apiUrl}${this.mainRoute}/get-all`);
  }

  public createChatBot(name: string, files: FileUploadModel[]): Observable<string> {
    const formData = new FormData();
    formData.append("name", name);

    files.forEach((item, index) => {
      formData.append(`files[${index}].file`, item.file);
      formData.append(`files[${index}].description`, item.description);
    });

    return this._http.post<string>(`${this.apiUrl}${this.mainRoute}/create-bot`, formData);
  }

}
