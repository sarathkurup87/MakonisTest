import { Component, Inject } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  _http: HttpClient;
  _baseUrl: string = ""
  userForm = this.formBuilder.group({
    FirstName: '',
    LastName: ''
  });

  constructor(private formBuilder: FormBuilder, http: HttpClient, @Inject('BASE_URL') baseUrl: string, private router: Router) { this._http = http; this._baseUrl = baseUrl}

  onSubmit(): void {
    debugger;
    this._http
      .post(this._baseUrl + 'test/saveData', this.userForm.value)
      .subscribe({
        next: (response) => { debugger; console.log(response) },
        error: (error) => { debugger; console.log(error) },
      });
    console.warn('Your order has been submitted', this.userForm.value);
    this.userForm.reset();
    setTimeout(() => { this.router.navigate(['/fetch-data']); },1000);
  }
}

interface User {
  FirstName: string;
  LastName: string;
}
