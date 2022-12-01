import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  form!: FormGroup 
 
  
  submitted = false;

  constructor(private router: Router, private http: HttpClient, private formBuilder: FormBuilder) { }
  
  ngOnInit(): void {
    this.form = this.formBuilder.group({
      name:['',[ Validators.required]],
    });
  }

  onSubmit()
  {
   this.submitted = true;
   
   if (this.form.invalid) {
    return
   }
   console.log(this.form.value);
   this.http.post<any>("https://localhost:7160/api/Guess/" + this.form.value.name,{} )
   .subscribe(response => { 
    console.log(response)
    alert("Success");

        this.router.navigate(['game', response.id]);
  });
  }

}
