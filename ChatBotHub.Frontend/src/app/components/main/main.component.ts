import { Component, inject, OnInit } from '@angular/core';
import { ChatBotService } from 'src/app/services/chat-bot.service';
import { ChatBotModel } from 'src/app/types/chat-bot.model';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {

  private readonly _chatBotService = inject(ChatBotService);

  public chatBots: ChatBotModel[] = [];

  public ngOnInit(): void {
    this._chatBotService.getChatBots().subscribe(chatBots => {
      this.chatBots = chatBots;
    });
  }

  
}
