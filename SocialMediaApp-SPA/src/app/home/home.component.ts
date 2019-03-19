import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
   registerMode = false;
   values: any; // any, is sort of like var in javascript, no type specification needed.

  constructor(private http: HttpClient) { }

  ngOnInit() { // After component is initialized.
    this.getValues();
  }

  registerToggle() {
    this.registerMode = !this.registerMode;
  }

  getValues() { // This function needs to be called when the component loads.
    this.http.get('http://localhost:5000/api/values').subscribe(response => {
      this.values = response;
    }, error => {
        console.log(error);
    });
  }
}
