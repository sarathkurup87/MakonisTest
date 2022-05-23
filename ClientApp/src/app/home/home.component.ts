import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  userForm: FormGroup;
  _http: HttpClient;
  _baseUrl: string = "";
  isSubmitted = false;

  constructor(private formBuilder: FormBuilder, http: HttpClient, @Inject('BASE_URL') baseUrl: string, private router: Router) {
    this._http = http; this._baseUrl = baseUrl;
    this.userForm = this.formBuilder.group({
      FirstName: ['', Validators.required],
      LastName: ['', Validators.required]
    });
  }

  onSubmit(): void {
    this.isSubmitted = true;
    if (this.userForm.invalid) {
      return;
    }
    this.userForm.reset();
    this._http
      .post(this._baseUrl + 'test/saveData', this.userForm.value)
      .subscribe({
        next: (response) => { debugger; console.log(response) },
        error: (error) => { debugger; console.log(error) },
      });
    console.warn('Your order has been submitted', this.userForm.value);
    setTimeout(() => { this.router.navigate(['/fetch-data']); }, 1000);
  }

  get formControls() { return this.userForm.controls; }

}
