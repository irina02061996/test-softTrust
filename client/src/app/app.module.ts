import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import {HttpClientModule} from '@angular/common/http';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';

import { AppComponent } from './app.component';
import { ThemesService } from './services/themes.service';
import { UsersService } from './services/users.service';
import { MessagesService } from './services/messages.service';



@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  providers: [
    ThemesService,
    UsersService,
    MessagesService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
