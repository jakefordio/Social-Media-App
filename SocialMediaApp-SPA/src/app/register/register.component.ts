import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
@Input() valuesFromHome: any; // Must match variable name from Html file
model: any = {};

  constructor() { }

  ngOnInit() {
  }

  register() {
    console.log('Register here.');
  }

  cancel() {
    console.log('Cancelled.');
  }

}
