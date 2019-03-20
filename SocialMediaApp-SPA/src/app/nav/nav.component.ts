import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-nav', // This selector is what we use as a tag in app.component.html
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {}; // This will store our username and password, to pass to another method. Object name is model, with a type of "any"

  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      console.log('Logged in successfully!');
    }, error => {
      console.log('Failed to login, bad credentials.');
    });
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !!token; // Shorthand: if there is a value in token, it will return true, else, return false.
  }

  logout() {
    localStorage.removeItem('token');
    console.log('Logged out.');
  }
}
