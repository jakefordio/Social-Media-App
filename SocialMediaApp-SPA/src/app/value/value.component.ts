import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-value',
  templateUrl: './value.component.html',
  styleUrls: ['./value.component.css']
})
export class ValueComponent implements OnInit {
  values: any; // any, is sort of like var in javascript, no type specification needed.

  constructor(private http: HttpClient) { } // Injecting HttpClient so that we can make HTTP Requests and get responses.

  ngOnInit() { // After component is initialized.
    this.getValues();
  }

  getValues() { // This function needs to be called when the component loads.
    this.http.get('http://localhost:5000/api/values').subscribe(response => {
      this.values = response;
    }, error => {
        console.log(error);
    });
  }

}
