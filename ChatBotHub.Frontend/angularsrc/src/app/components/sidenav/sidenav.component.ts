import { Component, EventEmitter, inject, Output } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';
import { ChatBotStateService } from 'src/app/services/chat-bot-state.service';
import { JwtTokenService } from 'src/app/services/jwt-token.service';
import { ChatBotModel } from 'src/app/types/chat-bot.model';
import { UserModel } from 'src/app/types/user.model';

@Component({
  selector: 'app-sidenav',
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.scss']
})
export class SidenavComponent {
  
  private readonly _chatBotStateService = inject(ChatBotStateService);
  private readonly _jwtTokenService = inject(JwtTokenService);
  private readonly _router = inject(Router);
  
  @Output() toggleDrawer = new EventEmitter<boolean>();

  public user: UserModel | null = null;
  public drawerOpened: boolean = true;

  public chatBots: Observable<ChatBotModel[]> = new Observable<ChatBotModel[]>();

  public ngOnInit(): void {
    this.user = this._jwtTokenService.getUserData();
    this.chatBots = this._chatBotStateService.chatBots;
    
    this._chatBotStateService.loadChatBots().subscribe();
  }

  public toggleDrawerClick(): void {
    this.drawerOpened = !this.drawerOpened;
    this.toggleDrawer.emit(this.drawerOpened);
  }

  public logoutClick(): void {
    this._jwtTokenService.removeToken();
    this._router.navigate(['/login']);

  }

}
