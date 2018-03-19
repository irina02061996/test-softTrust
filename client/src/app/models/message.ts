import {User} from './user';
import {Theme} from './theme';

export class Message {
  constructor(public theme: Theme,
              public user: User,
              public content: string,
              public id?: number) {
  }
}
