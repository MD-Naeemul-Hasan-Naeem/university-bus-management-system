import { Component } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { AuthService } from '../../../core/services/auth';
import { Router } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-register',
  imports: [FormsModule,
    HttpClientModule, ],
  templateUrl: './register.html'
})
export class RegisterComponent {
  model = { FullName: '', Email: '', Password: '', UserType: '' };

  constructor(private auth: AuthService, private router: Router) {}

  onSubmit(form: NgForm) {
    if (form.valid) {
      this.auth.register(this.model).subscribe({
        next: (res: any) => {
          alert('Registration successful!');
          this.router.navigate(['/login']);
        },
        error: (err) => {
          alert(err.error.message || 'Registration failed');
        }
      });
    }
  }
}
