import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Game } from '../game.model';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.scss']
})
export class GameComponent implements OnInit {

  form!: FormGroup 
  submitted = false;
  play = true;
  result?: boolean = false;
  rendomNUmber?: number; 
  gameResult: Game[] = [];
  number?: number;
  constructor(private route: ActivatedRoute, private router: Router, private http: HttpClient, private formBuilder: FormBuilder) {
      this.userId = route.snapshot.params['id'];
   }

  userId: number;
  ngOnInit(): void {
    this.form = this.formBuilder.group({
      number1:['', [Validators.required, Validators.max(9)]],
      number2:['', [Validators.required, Validators.max(9)]],
      number3:['', [Validators.required, Validators.max(9)]],
      number4:['', [Validators.required, Validators.max(9)]],
    });
  }

  onSubmit()
  {
   this.submitted = true;
   
   if (this.form.invalid) {
    return
   }
   this.number =parseInt(  String(this.form.value.number1) + String(this.form.value.number2) + String(this.form.value.number3) + String(this.form.value.number4), 10) ;   
   this.http.post<Game[]>("https://localhost:7160/api/Guess/GameStart/" + this.userId,this.number )
   .subscribe(response => { 
    this.gameResult = response;
    
    if (this.gameResult) {
      this.result = this.gameResult[0].checkNumber;      
    }

    if (this.gameResult[0].checkNumber) {
      this.play = false
      this.rendomNUmber = this.gameResult[0].number;
     }
     if (this.gameResult.length == 8) {
      this.play = false
      this.rendomNUmber = this.gameResult[0].number;  
     } 
  });
   
   
  }

}
