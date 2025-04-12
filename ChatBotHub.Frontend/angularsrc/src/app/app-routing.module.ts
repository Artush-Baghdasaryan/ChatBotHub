import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/auth/login/login.component';
import { RegisterComponent } from './components/auth/register/register.component';
import { MainComponent } from './components/main/main.component';
import { authGuardGuard } from './guards/auth-guard.guard';
import { ChatBotComponent } from './components/chat-bot/chat-bot.component';
import { CreateBotComponent } from './components/create-bot/create-bot.component';

const routes: Routes = [
  {
    path: "main",
    component: MainComponent,
    canActivate: [authGuardGuard]
  },
  {
    path: "chat-bot/:id",
    component: ChatBotComponent,
    canActivate: [authGuardGuard]
  },
  {
    path: "login",
    component: LoginComponent
  },
  {
    path: "register",
    component: RegisterComponent
  },
  {
    path: "create-bot",
    component: CreateBotComponent,
    canActivate: [authGuardGuard]
  },
  {
    path: "",
    redirectTo: "main",
    pathMatch: "full"
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
