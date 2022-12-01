import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from '../user.model';

@Component({
  selector: 'app-leader',
  templateUrl: './leader.component.html',
  styleUrls: ['./leader.component.scss']
})
export class LeaderComponent implements OnInit {
  
  leaderResult: User[] = [];
  min:number|undefined = 0; 
  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.http.get<User[]>("https://localhost:7160/api/Guess/"+this.min)
    .subscribe(response => { console.log(response), this.leaderResult = response , console.log(this.leaderResult)});
  }


  eventButton():void{
    if(!this.min) return;
    console.log("work......");
    this.http.get<User[]>("https://localhost:7160/api/Guess/"+this.min)
    .subscribe(response => { console.log(response), this.leaderResult = response , console.log(this.leaderResult)});  
  }
  
}
