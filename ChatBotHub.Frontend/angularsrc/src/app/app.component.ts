import { Component, inject } from '@angular/core';
import { JwtTokenService } from './services/jwt-token.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'ChatBotHub.Frontend';

  private readonly _jwtTokenService = inject(JwtTokenService);

  public drawerOpened: boolean = true;

  public isLoggedIn(): boolean {
    return this._jwtTokenService.getToken() !== null;
  }

  public getDrawerContentMargin(): string {
    if (!this.isLoggedIn()) {
      return '0px';
    }

    return this.drawerOpened ? '280px' : '100px';
  }

}
