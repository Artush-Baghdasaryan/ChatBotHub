import { Component, Input } from '@angular/core';
import { ChatBotModel } from 'src/app/types/chat-bot.model';

@Component({
  selector: 'app-chat-files',
  templateUrl: './chat-files.component.html',
  styleUrls: ['./chat-files.component.scss']
})
export class ChatFilesComponent {

  @Input() public chatBot: ChatBotModel | null = null;
  
}
