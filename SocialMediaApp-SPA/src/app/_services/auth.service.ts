import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

// Unlike components, services are not automagically injectable, so we have to include
// this @Injectable decorator.
@Injectable({
  // providedIn tells our service and any components using this service which module is providing this service.
  // In this case, the root module is providing this service (root module is our app module: app.module.ts)
  // We also need to tell our app module about this service, the provider array of app.module.ts
  providedIn: 'root'
})
export class AuthService {

baseURL = 'http://localhost:5000/api/auth/';

constructor(private http: HttpClient) { }

  // Login method notes: For the 3rd parameter of post method, typically you need to specify options
  // that describe the type of content we are sending in our HTTP request to our API.
  // It depends on the API. Since we are using ASP.NET Core as our API, it expects
  // to receive the content as 'Application/JSON', so we don't need to specify a header.
  // We also don't need to specify authorization header, because this is our login, and
  // anyone should be able to get to this page. Anonymous access. So, no 3rd parameter.
  // This request is going to return a JWT token in the response. It is going to return an observable:
  // See: https://angular-2-training-book.rangle.io/handout/observables/ for definition.
  // Anytime we use observables, we need to use RXJS Operators: https://angular.io/guide/rx-library
  // The pipe method links these operators together (in this case, just 1 operator: map())
login(model: any) {
  return this.http.post(this.baseURL + 'login', model).pipe(
    map((response: any) => { // Arrow Function... Same as function(response: any){}
      const user = response; // The response is our token object.
      if (user) {
        localStorage.setItem('token', user.token); // localstorage is a part of the browser classes... window.localStorage
      }
    })
  );
}
}
