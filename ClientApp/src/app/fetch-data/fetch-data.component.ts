import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public users: any;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<User[]>(baseUrl + 'test/getData').subscribe(result => {
      debugger;
      this.users = result;
      result.forEach(x => console.log(x));
    }, error => console.error(error));

      //.pipe(map((data: any) => data.result),
       // catchError(error => { return throwError('Its a Trap!') }));

  }
}

interface User {
  FirstName: string;
  LastName: string;
}
