import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';

import {ThemesService} from './services/themes.service';
import {UsersService} from './services/users.service';
import {MessagesService} from './services/messages.service';

import {Theme} from './models/theme';
import {User} from './models/user';
import {Message} from './models/message';

import {CreateUser} from './models/create-user';
import {CreateMessage} from './models/create-message';
import {ErrorMessage} from './models/error-message';

declare let $: any;

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})

export class AppComponent implements OnInit {

  currentTheme;

  user;
  message;

  themes: Theme[];
  users: User[];
  messages: Message[];

  errorMessage: ErrorMessage;

  constructor(private themesService: ThemesService,
              private usersService: UsersService,
              private messagesService: MessagesService) {
  }

  form: FormGroup;


  onSubmit() {
    this.getUsers();

    let i;
    i = 0;

    let isExistUser;
    isExistUser = false;

    while (i < this.users.length) {
      if (this.users[i].email === this.form.value.email &&
        this.users[i].phone === this.form.value.phone) {
        this.user = this.users[i];
        isExistUser = true;
      }
      i++;
    }

    if (!isExistUser) {
      if (this.form.valid) {

        i = 0;
        while (i < this.themes.length) {
          if (this.themes[i].name === this.form.value.theme) {
            this.currentTheme = this.themes[i];
          }
          i++;
        }

        let createUser: CreateUser;
        createUser = {
          'name': this.form.value.name,
          'phone': this.form.value.phone,
          'email': this.form.value.email
        };

        let someStream;
        someStream = this.usersService.createUser(createUser)
          .flatMap((user: User) => {
            this.user = user;

            let createMessage: CreateMessage;
            if (this.user) {
              createMessage = {
                'content': this.form.value.message,
                'themeId': this.currentTheme.id,
                'userId': this.user.id
              };
            }
            return this.messagesService.createMessage(createMessage);
          });

        someStream
          .subscribe(
            (message: Message) => {
              this.message = message;

              $('.info-user').css({
                'display': 'block'
              });

              $('.info-form').css({
                'display': 'none'
              });
            });
      } else {
        this.showMessage({
          text: 'Форма содержит ошибки',
          type: 'danger'
        });
      }
    } else {
      let createMessage: CreateMessage;
      createMessage = {
        'content': this.form.value.message,
        'themeId': this.currentTheme.id,
        'userId': this.user.id
      };
      this.messagesService.createMessage(createMessage)
        .subscribe(
          (message: Message) => {
            this.message = message;

            $('.info-user').css({
              'display': 'block'
            });

            $('.info-form').css({
              'display': 'none'
            });
          });
    }
  }


  ngOnInit() {
    $('.info-user').css({
      'display': 'none'
    });

    $(document).ready(function () {
      $('#phone').mask('+7(999) 999-99-99', {placeholder: '+7(999) 999-99-99'});
    });

    this.form = new FormGroup({
      'email': new FormControl(
        null,
        [Validators.required, Validators.email]),
      'name': new FormControl(
        null,
        [Validators.required]),
      'phone': new FormControl(
        null,
        [Validators.required]),
      'theme': new FormControl(
        null,
        []),
      'message': new FormControl(
        null,
        [Validators.required])
    });

    this.getThemes();
    this.getUsers();
    this.getMessages();
  }


  getThemes() {
    this.themesService.getThemes()
      .subscribe((themes: Theme[]) => {
        this.themes = themes;
        if (this.themes) {
          this.currentTheme = this.themes[0];
        }
      });
  }

  getUsers() {
    this.usersService.getUsers()
      .subscribe((users: User[]) => {
        this.users = users;
      });
  }

  getMessages() {
    this.messagesService.getMessages()
      .subscribe((messages: Message[]) => {
        this.messages = messages;
      });
  }


  showMessage(errorMessage: ErrorMessage) {
    this.errorMessage = errorMessage;

    $('.alert').css({
      'display': 'block'
    });

    window.setTimeout(() => {
      $('.alert').css({
        'display': 'none'
      });
    }, 3000);
  }

  returnToForm() {
    $('.info-user').css({
      'display': 'none'
    });

    $('.info-form').css({
      'display': 'block'
    });
  }
}


