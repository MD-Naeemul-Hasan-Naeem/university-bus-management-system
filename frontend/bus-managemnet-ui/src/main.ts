import { bootstrapApplication } from '@angular/platform-browser';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { App } from './app/app';
import { LoginComponent } from './app/features/auth/login/login';
import { RegisterComponent } from './app/features/auth/register/register';
import { AdminDashboardComponent } from './app/admin/admin-dashboard/admin-dashboard';
import { DashboardHomeComponent } from './app/admin/dashboard-home/dashboard-home';

bootstrapApplication(App, {
  providers: [
    provideHttpClient(
      withInterceptors([
        (req, next) => {
          const token = localStorage.getItem('token');

          if (token) {
            req = req.clone({
              setHeaders: {
                Authorization: `Bearer ${token}`
              }
            });
          }

          return next(req);
        }
      ])
    ),

    provideRouter([
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
      { path: 'admin-dashboard', component: AdminDashboardComponent },
       { path: 'dashboard-home', component: DashboardHomeComponent },
      { path: '**', redirectTo: 'login' }
    ])
  ]
});