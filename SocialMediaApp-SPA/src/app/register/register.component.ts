import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
// 4 parts to output property:
// 1. Output property itself -> Assign it a new EventEmitter
// 2. Add to our cancel method: this.cancelRegister.emit(false);
// 3. Go back to home component add this to app-register tag: (cancelRegister)="cancelRegisterMode($event)"
// 4. Create the method cancelRegisterMode($event) inside of our home component
@Output() cancelRegister = new EventEmitter(); //
model: any = {};

  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  register() {
    // For success parameter in subscribe, we leave parentheses blank because we are
    // not actually using anything from this response inside what we are doing after this is successful.
    this.authService.register(this.model).subscribe( () => {
      console.log('Registration successful.');
    }, error => {
      console.log(error);
    });

  }

  cancel() {
    this.cancelRegister.emit(false); // This doesn't have to be a boolean, could be an object or any data type
    console.log('Cancelled.');
  }

}
