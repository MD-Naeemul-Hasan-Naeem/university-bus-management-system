import { Component } from '@angular/core';
import { NgForm, FormsModule } from '@angular/forms';
import { AuthService } from '../../../core/services/auth';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    RouterModule
  ],
  templateUrl: './login.html'
})
export class LoginComponent {

  model = {
  email: '',
  password: ''
};

  constructor(
    private auth: AuthService,
    private router: Router
  ) {}

  onSubmit(form: NgForm) {

    if (!form.valid) return;

    this.auth.login(this.model).subscribe({
      next: () => {

        const role = this.auth.getRole();

        if (role === 'Admin') {
          this.router.navigate(['/admin']);
        }
        else if (role === 'Employee') {
          this.router.navigate(['/employee']);
        }
        else {
          this.router.navigate(['/user']);
        }

      },
      error: (err) => {
        alert(err.error?.message || 'Login failed');
      }
    });
  }
}