import { Component, inject, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ChatBotService } from 'src/app/services/chat-bot.service';
import { ChatBotModel } from 'src/app/types/chat-bot.model';

@Component({
  selector: 'app-chat-bot',
  templateUrl: './chat-bot.component.html',
  styleUrls: ['./chat-bot.component.scss']
})
export class ChatBotComponent implements OnInit {

  private readonly _chatBotService = inject(ChatBotService);
  private readonly _route = inject(ActivatedRoute);

  public messageForm: FormGroup = new FormGroup({
    message: new FormControl("")
  });

  public chatBot: ChatBotModel | null = null;

  public messages: Message[] = [];

  public ngOnInit(): void {
    const botId = this._route.snapshot.params['id'];
    this._chatBotService.getChatBot(botId).subscribe(chatBot => {
      this.chatBot = chatBot;
    });
  }

  public sendMessage(): void {
    const message = this.messageForm.get('message')?.value;
    if (!message || message.length === 0) {
      return;
    }

    this.messages.push({
      message,
      author: "user"
    });

    this.messages.push({
      message: `${message} from bot 😙`,
      author: "bot"
    });

    this.messageForm.get('message')?.setValue('');
  }
  
}

export interface Message {
  message: string;
  author: "user" | "bot";
}

