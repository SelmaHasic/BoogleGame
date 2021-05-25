import { Component, Inject  } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { timer } from "rxjs";
import { getBaseUrl } from 'src/main';

@Component({
  selector: 'app-boogle-component',
  templateUrl: './boogle.component.html'
})

export class BoogleComponent {
  public userNamesEntered = true;
  public isGameRunning = false;
  public usernameFirst: string = '';
  public usernameSecond: string = '';
  baseURL: string = 'https://localhost:44395/api/BoogleGame';
  newWordFirst: string = '';
  allWordsFirst: string[] = [];
  newWordSecond: string = '';
  allWordsSecond: string[] = [];
  public allIn: In[] = [];
  public cubicDice: object;
  public time = 180;
  public isRunning = false;
  public isGameFinished = false;
  public allOut: Out;
  public isEnabled = false;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
   
    this.http.get(baseUrl + 'api/BoogleGame').subscribe(result => {
      this.cubicDice = result;
    }, error => console.error(error));
  }

  ngOnInit() {
    timer(0, 1000).subscribe(ellapsedCycles => {
      if (this.isRunning) {
        if (this.time > 0) {
          this.time--;
        }
        else {
          this.isEnabled = true;
        }
      }
    });
  }

  toggleTimer() {
    this.isRunning = !this.isRunning;
  }

  public addWordFirst() {
    this.allWordsFirst.push(this.newWordFirst);
    //Reset input
    this.newWordFirst = '';
  }

  public addWordSecond() {
    this.allWordsSecond.push(this.newWordSecond);
    //Reset input
    this.newWordSecond = '';
  }

  public submit() {
    this.userNamesEntered = false;
    this.isGameRunning = true;
    this.toggleTimer();
  }

  getBoogleGameResults() {
    const headers = { 'content-type': 'application/json' }
    const body = JSON.stringify(this.allIn);

    this.http.post<Out>(this.baseURL, body, { 'headers': headers }).subscribe(result => {
      this.allOut = result;
    }, error => console.error(error));
  }


  public submitResults() {

    let InFirst: In = {
      Id : 1,
      UserName: this.usernameFirst,
      Words: this.allWordsFirst
    }

    let InSecond: In = {
      Id: 2,
      UserName: this.usernameSecond,
      Words: this.allWordsSecond
    }

  
    this.allIn.push(InFirst);
    this.allIn.push(InSecond);

    const headers = { 'content-type': 'application/json' }
    const body = JSON.stringify(this.allIn);
    this.getBoogleGameResults();
    this.isGameFinished = true;
    this.isGameRunning = false;
  }
}

interface In {
  Id: number;
  UserName: string;
  Words: string[];
}


interface Out {
  UserName: string;
  Score: number;
}
