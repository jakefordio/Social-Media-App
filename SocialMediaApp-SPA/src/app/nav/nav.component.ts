import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-nav', // This selector is what we use as a tag in app.component.html
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {}; // This will store our username and password, to pass to another method. Object name is model, with a type of "any"

  constructor() { }

  ngOnInit() {
  }

  login() {
    console.log(this.model);
  }

}
