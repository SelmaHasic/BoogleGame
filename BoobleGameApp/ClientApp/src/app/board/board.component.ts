import { Component } from '@angular/core';

@Component({
  selector: 'app-boogle-component',
  templateUrl: './board.component.html'
})

export class BoardComponent {
  public usernameFirst: string = '';
  public usernameSecond: string = '';


  public submit() {
    this.usernameFirst;
    console.log('it does nothing', this.usernameFirst);
    
  }
}
