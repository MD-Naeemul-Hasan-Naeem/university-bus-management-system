import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthService } from '../../../core/services/auth';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ FormsModule,
    HttpClientModule, ],
  templateUrl: './login.html'

})
export class LoginComponent {
  model = { Email: '', Password: '' };

  constructor(private auth: AuthService, private router: Router) {}

  onSubmit(form: NgForm) {
    if (form.valid) {
      this.auth.login(this.model).subscribe({
        next: (res: any) => {
          alert(res.message);
          this.auth.saveToken(res.token);
          localStorage.setItem('role', res.role);

          // Redirect based on role
          if (res.role === 'Admin') this.router.navigate(['/admin']);
          else if (res.role === 'Employee') this.router.navigate(['/employee']);
          else this.router.navigate(['/user']);
        },
        error: (err) => {
          alert(err.error.message || 'Login failed');
        }
      });
    }
  }
}
